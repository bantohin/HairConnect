namespace HairConnect.Web.Models.Profiles
{
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;

    public class UserDetailsModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Rating { get; set; }

        public byte[] ProfilePicture { get; set; }

        public List<Picture> Pictures { get; set; } = new List<Picture>();
    }
}
