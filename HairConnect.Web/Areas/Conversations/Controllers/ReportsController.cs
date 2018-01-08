namespace HairConnect.Web.Areas.Conversations.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using System.Collections.Generic;
    using AutoMapper;
    using Models.Reports;
    using Services.Models.Reports;

    public class ReportsController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly IReportService reportService;

        public ReportsController(UserManager<User> userManager, IReportService reportService)
        {
            this.userManager = userManager;
            this.reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> FileReport(string id)
        {
            if (this.userManager.FindByIdAsync(id).Result == null)
            {
                this.TempData.AddErrorMessage("No such user exists.");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            FileReportModel model = new FileReportModel()
            {
                ReportedUserId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FileReport(FileReportModel model)
        {
            if (this.userManager.FindByIdAsync(model.ReportedUserId).Result == null)
            {
                this.TempData.AddErrorMessage("No such user exists.");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            User currentUser = await this.userManager.FindByEmailAsync(this.User.Identity.Name);
            await this.reportService.CreateReport(currentUser.Id, model.ReportedUserId, model.Content);
            this.TempData.AddSuccessMessage("Report successfully filed. It will be reviewed by an admin soon.");

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize(Roles = WebConstants.AdminRole)]
        public async Task<IActionResult> ListReports()
        {
            return View(await this.reportService.GetAllReports());
        }
    }
}
