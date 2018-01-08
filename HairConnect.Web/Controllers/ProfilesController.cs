namespace HairConnect.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Services.Interfaces;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Data.Models;
    using System.Linq;
    using HairConnect.Web.Infrastructure.Extensions;
    using Microsoft.Extensions.Caching.Memory;

    [Authorize]
    public class ProfilesController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly IPictureService pictureService;
        private readonly IMemoryCache memoryCache;

        public ProfilesController(IUserService userService, UserManager<User> userManager, IPictureService pictureService, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.pictureService = pictureService;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> ListProfiles()
        {
            return View(await this.userService.GetUsersToList());
        }

        public async Task<IActionResult> Details(string id)
        {
            User user = await this.userService.GetUserById(id);
            
            if (user == null)
            {
                this.TempData.AddErrorMessage("No such user exists.");
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<string> roles = await this.userManager.GetRolesAsync(user);

            if (!roles.Contains(WebConstants.HairdresserRole))
            {
                this.TempData.AddErrorMessage("The user is not a hairdresser.");
                return RedirectToAction("Index", "Home");
            }

            return View(this.userService.GetUserDetails(user));
        }

        public async Task<IActionResult> Upvote(string id)
        {
            await this.userService.UpRating(id);

            return RedirectToAction("Details", new { id });
        }

        public async Task<IActionResult> Downvote(string id)
        {
            await this.userService.DownRating(id);

            return RedirectToAction("Details", new { id });
        }

        public string GetPicture(byte[] pictureContent)
        {
            return this.pictureService.DisplayPicture(pictureContent);
        }
    }
}
