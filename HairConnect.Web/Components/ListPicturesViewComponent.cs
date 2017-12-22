namespace HairConnect.Web.Controllers.Components
{
    using Data.Models;
    using Models.Pictures;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using AutoMapper;
    using Services.Interfaces;
    using System.Collections.Generic;

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
            List<Picture> pictures = this.userService.GetUserById(id).Result.Pictures;
            List<ListPicturesModel> model = Mapper.Map<List<ListPicturesModel>>(pictures);

            return View(model);
        }
    }
}
