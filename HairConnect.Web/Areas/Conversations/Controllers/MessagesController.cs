namespace HairConnect.Web.Areas.Conversations.Controllers
{
    using Data.Models;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Infrastructure.Extensions;
    using System.Collections.Generic;
    using System.Linq;
    using Services.Models.Messages;
    using Models.Messages;

    public class MessagesController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly IMessageService messageService;

        public MessagesController(UserManager<User> userManager, IMessageService messageService)
        {
            this.userManager = userManager;
            this.messageService = messageService;
        }

        public async Task<IActionResult> ShowConversation(string receiverId)
        {
            User currentUser = await this.userManager.FindByEmailAsync(this.User.Identity.Name);

            if (currentUser.Id == receiverId)
            {
                this.TempData.AddErrorMessage("You cannot message yourself.");
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            
            if (!this.messageService.ConversationExists(currentUser.Id, receiverId))
            {
                await this.messageService.CreateConversation(currentUser.Id, receiverId);
            }
            
            Conversation conversation = await this.messageService.GetConversation(currentUser.Id, receiverId);

            return View(this.messageService.GetConversationToShow(conversation));
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData.AddErrorMessage("You cannot send an empty message.");
                return View();
            }

            User currentUser = await this.userManager.FindByEmailAsync(this.User.Identity.Name);

            if (currentUser.Id == model.ReceiverId)
            {
                this.TempData.AddErrorMessage("You cannot message yourself.");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            await  this.messageService.CreateMessage(currentUser.Id, model.ReceiverId, model.Content);

            return RedirectToAction("ShowConversation", new { receiverId = model.ReceiverId });
        }

        public async Task<IActionResult> DeleteMessages(string receiverId)
        {
            User currentUser = await this.userManager.FindByEmailAsync(this.User.Identity.Name);
            Conversation conversation = await this.messageService.GetConversation(currentUser.Id, receiverId);

            if (conversation == null)
            {
                this.TempData.AddErrorMessage("This conversation does not exist.");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            await this.messageService.DeleteMessages(conversation);

            this.TempData.AddSuccessMessage("Conversation successfully deleted.");
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> ListConversations(string id)
        {
            User user = await this.userManager.FindByEmailAsync(this.User.Identity.Name);

            if (user.Id != id || user == null)
            {
                this.TempData.AddErrorMessage("You do not have access to this page.");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            List<ListConversationModel> conversations = this.messageService.GetConversationsForUser(id).Result.ToList();

            return View(conversations);
        }
    }
}
