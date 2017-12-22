namespace HairConnect.Web.Areas.Conversations.Models.Reports
{
    using Common.Mapping;
    using Data.Models;

    public class ListReportsModel : IMapFrom<Report>
    {
        public string Content { get; set; }

        public User Sender { get; set; }

        public User ReportedUser { get; set; }
    }
}
