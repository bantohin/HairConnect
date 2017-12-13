namespace HairConnect.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 20)]
        public string LastName { get; set; }
    }
}
