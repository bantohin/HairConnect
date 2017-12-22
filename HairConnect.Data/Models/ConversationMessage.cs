namespace HairConnect.Data.Models
{
    public class ConversationMessage
    {
        public int ConversationId { get; set; }

        public Conversation Conversation { get; set; }

        public int MessageId { get; set; }

        public Message Message { get; set; }
    }
}
