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
    using AutoMapper;
    using Models.Profiles;

    [Authorize]
    public class ProfilesController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public ProfilesController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ListProfiles()
        {
            List<User> users = new List<User>();

            foreach (User profile in await this.userService.GetAllUsers())
            {
                IEnumerable<string> roles = await this.userManager.GetRolesAsync(profile);
                if (roles.Contains(WebConstants.HairdresserRole))
                {
                    users.Add(profile);
                }
            }

            IEnumerable<UserListingModel> profiles = users.Select(Mapper.Map<User, UserListingModel>);

            return View(profiles);
        }

        public async Task<IActionResult> Details(string id)
        {
            User user = await this.userService.GetUserById(id);
            IEnumerable<string> roles = await this.userManager.GetRolesAsync(user);

            if (user == null || !roles.Contains(WebConstants.HairdresserRole))
            {
                return NotFound();
            }

            UserDetailsModel profile = Mapper.Map<User, UserDetailsModel>(user);

            return View(profile);
        }

        public FileContentResult GetProfilePicture(byte[] pictureContent)
        {
            if (pictureContent != null)
            {
                return new FileContentResult(pictureContent, "image/jpeg");
            }

            return null;
        }
    }
}
