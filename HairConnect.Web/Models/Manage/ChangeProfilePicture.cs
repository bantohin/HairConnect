namespace HairConnect.Web.Models.Manage
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class ChangeProfilePicture
    {
        [Display(Name = "Profile picture")]
        public IFormFile ProfilePictureFile { get; set; }

        public byte[] ProfilePictureContent { get; set; }

        public string StatusMessage { get; set; }
    }
}
