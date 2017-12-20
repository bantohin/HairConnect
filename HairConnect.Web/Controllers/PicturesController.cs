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
        public async Task<IActionResult> AddPicture() => View();        

        [HttpPost]
        public async Task<IActionResult> AddPicture(AddPictureModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = await this.userManager.FindByEmailAsync(this.User.Identity.Name);
            await this.pictureService.CreatePicture(user.Id, model.Picture);

            return RedirectToAction("Details", "Profiles", new { id = user.Id });
        }
    }
}
