namespace HairConnect.Web.Models.Pictures
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ListPicturesModel : IMapFrom<User>, IHaveCustomMapping
    {
        public List<byte[]> Pictures { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<User, ListPicturesModel>()
                .ForMember(u => u.Pictures, cfg => cfg.MapFrom(u => u.Pictures.Select(p => p.Content)));
        }
    }
}
