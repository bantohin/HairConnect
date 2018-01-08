namespace HairConnect.Services.Implementations
{
    using System.Threading.Tasks;
    using Data.Models;
    using Data;
    using Interfaces;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using Models.Reports;
    using AutoMapper.QueryableExtensions;

    public class ReportService : IReportService
    {
        private readonly HairConnectDbContext db;

        public ReportService(HairConnectDbContext db)
        {
            this.db = db;
        }

        public async Task CreateReport(string senderId, string reportedId, string content)
        {
            Report report = new Report()
            {
                Content = content,
                ReportedUserId = reportedId,
                SenderId = senderId
            };

            await this.db.Reports.AddAsync(report);

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAllReportsFromUser(User user)
        {
            IEnumerable<Report> reports = this.db.Reports.Where(r => r.SenderId == user.Id || r.ReportedUserId == user.Id);
            this.db.Reports.RemoveRange(reports);

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ListReportsModel>> GetAllReports()
        {
            IEnumerable<ListReportsModel> reports = await this.db
                .Reports
                .Include(r => r.Sender)
                .Include(r => r.ReportedUser)
                .ProjectTo<ListReportsModel>()
                .ToListAsync();

            return reports;
        }
    }
}
