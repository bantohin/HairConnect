namespace HairConnect.Web.Models.Pictures
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddPictureModel
    {
        [Required]
        public List<IFormFile> Picture { get; set; }
    }
}
