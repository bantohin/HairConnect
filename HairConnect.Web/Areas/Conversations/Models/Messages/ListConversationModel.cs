
namespace HairConnect.Web.Areas.Conversations.Models
{
    using Common.Mapping;
    using Data.Models;

    public class ListConversationModel : IMapFrom<Conversation>
    {
        public User Receiver { get; set; }
    }
}
