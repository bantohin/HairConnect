namespace HairConnect.Web.Controllers
{
    using Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Data.Models;
    using Models.Pictures;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using HairConnect.Web.Infrastructure.Extensions;

    [Authorize]
    public class PicturesController : Controller
    {
        private readonly IUserService userService;
        private readonly IPictureService pictureService;
        private readonly UserManager<User> userManager;

        public PicturesController(IUserService userService, IPictureService pictureService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.pictureService = pictureService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult AddPicture() => View();        

        [HttpPost]
        public async Task<IActionResult> AddPicture(AddPictureModel model)
        {
            if (!ModelState.IsValid)
            {
                this.TempData.AddErrorMessage("Please fill out the form correctly.");
                return View();
            }

            User user = await this.userManager.FindByEmailAsync(this.User.Identity.Name);
            await this.pictureService.CreatePicture(user.Id, model.Picture);

            this.TempData.AddSuccessMessage("Picture added successfully.");
            return RedirectToAction("Details", "Profiles", new { id = user.Id });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePicture(int id)
        {
            if (!this.pictureService.PictureExists(id))
            {
                this.TempData.AddErrorMessage("No such picture exists.");
                return RedirectToAction("Index", "Home");
            }

            User owner = await this.pictureService.GetOwner(id);
            
            if (owner.Email != this.User.Identity.Name && !this.User.IsInRole(WebConstants.AdminRole))
            {
                this.TempData.AddErrorMessage("This picture does not belong to you.");
                return RedirectToAction("Index", "Home");
            }

            DeletePictureModel model = Mapper.Map<DeletePictureModel>(await this.pictureService.GetPictureById(id));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            if (!this.pictureService.PictureExists(id))
            {
                this.TempData.AddErrorMessage("No such picture exists.");
                return RedirectToAction("Index", "Home");
            }

            User owner = await this.pictureService.GetOwner(id);

            if (owner.Email != this.User.Identity.Name && !this.User.IsInRole(WebConstants.AdminRole))
            {
                this.TempData.AddErrorMessage("This picture does not belong to you.");
                return RedirectToAction("Index", "Home");
            }

            await this.pictureService.DeletePicture(id);

            this.TempData.AddSuccessMessage("Picture successfully deleted.");
            return RedirectToAction("Details", "Profiles", new { id = owner.Id});
        }
    }
}
