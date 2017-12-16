namespace HairConnect.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 20)]
        public string LastName { get; set; }
        
        public byte[] ProfilePicture { get; set; }

        public List<Picture> Pictures { get; set; } = new List<Picture>();

        public List<Conversation> ConversationsSent { get; set; } = new List<Conversation>();

        public List<Conversation> ConversationsReceived { get; set; } = new List<Conversation>();

        public List<Message> Messages { get; set; } = new List<Message>();

        public List<Report> FiledReports { get; set; } = new List<Report>();

        public List<Report> ReceivedReports { get; set; } = new List<Report>();
    }
}