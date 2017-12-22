namespace HairConnect.Web.Models.Pictures
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class ListPicturesModel : IMapFrom<Picture>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string OwnerEmail { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Picture, ListPicturesModel>()
                .ForMember(p => p.OwnerEmail, cfg => cfg.MapFrom(p => p.User.Email));
        }
    }
}
