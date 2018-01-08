namespace HairConnect.Services.Models.Reports
{
    using Data.Models;
    using Common.Mapping;

    public class ListReportsModel : IMapFrom<Report>
    {
        public User Sender { get; set; }

        public User ReportedUser { get; set; }

        public string Content { get; set; }
    }
}
