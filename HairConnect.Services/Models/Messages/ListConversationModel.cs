namespace HairConnect.Services.Models.Messages
{
    using Common.Mapping;
    using Data.Models;

    public class ListConversationModel : IMapFrom<Conversation>
    {
        public User Receiver { get; set; }
    }
}
