namespace HairConnect.Web.Areas.Conversations.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class ShowMessagesModel : IMapFrom<ConversationMessage>, IHaveCustomMapping
    {
        public string SenderId { get; set; }

        public string Content { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<ConversationMessage, ShowMessagesModel>()
                .ForMember(m => m.SenderId, cfg => cfg.MapFrom(m => m.Message.SenderId))
                .ForMember(m => m.Content, cfg => cfg.MapFrom(m => m.Message.Content));
        }
    }
}
