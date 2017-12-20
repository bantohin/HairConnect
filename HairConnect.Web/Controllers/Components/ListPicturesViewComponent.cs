namespace HairConnect.Web.Controllers.Components
{
    using Data.Models;
    using Models.Pictures;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using AutoMapper;
    using Services.Interfaces;

    [ViewComponent]
    public class ListPicturesViewComponent : ViewComponent
    {
        private readonly IUserService userService;

        public ListPicturesViewComponent(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            User user = await this.userService.GetUserById(id);
            ListPicturesModel model = Mapper.Map<ListPicturesModel>(user);

            return View(model);
        }
    }
}
