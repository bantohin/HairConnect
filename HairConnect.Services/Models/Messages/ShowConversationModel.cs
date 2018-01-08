namespace HairConnect.Services.Models.Messages
{
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;

    public class ShowConversationModel : IMapFrom<Conversation>
    {
        public User Sender { get; set; }

        public User Receiver { get; set; }

        public List<ShowMessagesModel> Messages { get; set; }
    }
}
