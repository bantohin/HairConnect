namespace HairConnect.Data.Models
{
    using System.Collections.Generic;

    public class Conversation
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReceiverId { get; set; }

        public User Receiver { get; set; }

        public List<ConversationMessage> Messages { get; set; } = new List<ConversationMessage>();
    }
}
