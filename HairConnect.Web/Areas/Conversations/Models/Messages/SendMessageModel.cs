namespace HairConnect.Web.Areas.Conversations.Models.Messages
{
    public class SendMessageModel
    {
        public string ReceiverId { get; set; }

        public string SenderId { get; set; }

        public string Content { get; set; }
    }
}
