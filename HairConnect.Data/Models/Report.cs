namespace HairConnect.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Report
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string Content { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReportedUserId { get; set; }

        public User ReportedUser { get; set; }
    }
}
