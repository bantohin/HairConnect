using System.ComponentModel.DataAnnotations;

namespace HairConnect.Web.Areas.Conversations.Models.Reports
{
    public class FileReportModel
    {
        public string ReportedUserId { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }
    }
}
