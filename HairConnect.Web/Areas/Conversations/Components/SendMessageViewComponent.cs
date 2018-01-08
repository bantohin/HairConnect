namespace HairConnect.Web.Areas.Conversations.Components
{
    using Models.Messages;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class SendMessageViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string receiverId)
        {
            this.ViewData["ReceiverId"] = receiverId;
            return this.View(new SendMessageModel());
        }
    }
}