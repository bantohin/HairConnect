namespace HairConnect.Web.Models.Pictures
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ListPicturesModel : IMapFrom<User>, IHaveCustomMapping
    {
        public List<ListPictureInfo> Pictures { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<User, ListPicturesModel>()
                .ForMember(u => u.Pictures, cfg => cfg.MapFrom(u => u.Pictures.Select(p => new ListPictureInfo
                {
                    Content = p.Content,
                    Id = p.Id,
                    OwnerEmail = p.User.Email
                })));
        }

        public class ListPictureInfo
        {

            public int Id { get; set; }

            public byte[] Content { get; set; }

            public string OwnerEmail { get; set; }
        }
    }
}
