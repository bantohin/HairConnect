namespace HairConnect.Web.Areas.Conversations.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SendMessageModel
    {
        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        public string ReceiverId { get; set; }
    }
}
