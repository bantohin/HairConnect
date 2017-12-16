namespace HairConnect.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Content { get; set; }
        
        public string SenderId { get; set; }

        public User Sender { get; set; }

        public int ConversationId { get; set; }

        public Conversation Conversation { get; set; }
    }
}
