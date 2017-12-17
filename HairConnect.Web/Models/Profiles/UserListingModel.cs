namespace HairConnect.Web.Models.Profiles
{
    using Common.Mapping;
    using Data.Models;

    public class UserListingModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Rating { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}
