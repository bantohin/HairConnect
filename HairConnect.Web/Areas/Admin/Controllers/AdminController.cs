namespace HairConnect.Web.Areas.Admin.Controllers
{
    using Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Services.Interfaces;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Data.Models;
    using System.Linq;
    using HairConnect.Web.Infrastructure.Extensions;

    public class AdminController : BaseController
    {
        private readonly IUserService userService;
        private readonly IPictureService pictureService;
        private readonly UserManager<User> userManager;

        public AdminController(IUserService userService, IPictureService pictureService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.pictureService = pictureService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ListAllUsers()
        {
            List<ListUsersModel> users = Mapper.Map<List<ListUsersModel>>(await this.userService.GetAllUsers());
            foreach (ListUsersModel user in users)
            {
                user.Roles = this.userManager.GetRolesAsync(await this.userManager.FindByIdAsync(user.Id)).Result.ToList();
            }

            return View(users);
        }

        public async Task<IActionResult> MakeAdmin(string id)
        {
            User user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                this.TempData.AddErrorMessage("No such user exists");
                return RedirectToAction("ListAllUsers");
            }

            if(await this.userManager.IsInRoleAsync(user, WebConstants.AdminRole))
            {
                this.TempData.AddErrorMessage("This user is already an admin.");
                return RedirectToAction("ListAllUsers");
            }

            await this.userManager.AddToRoleAsync(user, WebConstants.AdminRole);
            this.TempData.AddSuccessMessage($"User({user.Email}) successfully added as an admin.");

            return RedirectToAction("ListAllUsers");
        }

        public async Task<IActionResult> Ban(string id)
        {
            User user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                this.TempData.AddErrorMessage("No such user exists");
                return RedirectToAction("ListAllUsers");
            }

            if (await this.userManager.IsInRoleAsync(user, WebConstants.AdminRole))
            {
                this.TempData.AddErrorMessage("This user is an admin.");
                return RedirectToAction("ListAllUsers");
            }

            string email = user.Email;
            await this.pictureService.DeleteAllPicturesFromUser(user);
            await this.userManager.DeleteAsync(user);
            this.TempData.AddSuccessMessage($"User({email}) successfully deleted.");

            return RedirectToAction("ListAllUsers");
        }
    }
}
