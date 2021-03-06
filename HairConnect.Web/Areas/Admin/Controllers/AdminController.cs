﻿namespace HairConnect.Web.Areas.Admin.Controllers
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
        private readonly IReportService reportService;
        private readonly IMessageService messageService;
        private readonly UserManager<User> userManager;

        public AdminController(IUserService userService,
            IPictureService pictureService,
            IReportService reportService,
            IMessageService messageService,
            UserManager<User> userManager)
        {
            this.userService = userService;
            this.pictureService = pictureService;
            this.reportService = reportService;
            this.messageService = messageService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ListAllUsers()
        {
            return View(await this.userService.GetUsersToListAdmin());
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
            await this.messageService.DeleteAllMessagesAndConvosFromUser(user);
            await this.reportService.DeleteAllReportsFromUser(user);
            await this.userManager.DeleteAsync(user);
            this.TempData.AddSuccessMessage($"User({email}) successfully deleted.");

            return RedirectToAction("ListAllUsers");
        }
    }
}
