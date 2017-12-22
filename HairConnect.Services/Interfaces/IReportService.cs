namespace HairConnect.Services.Interfaces
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReportService
    {
        Task CreateReport(string senderId, string reportedId, string content);

        Task<IEnumerable<Report>> GetAllReports();

        Task DeleteAllReportsFromUser(User user);
    }
}
