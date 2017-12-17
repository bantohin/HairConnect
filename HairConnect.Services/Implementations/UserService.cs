namespace HairConnect.Services.Implementations
{
    using System.Collections.Generic;
    using Interfaces;
    using Data;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Data.Models;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly HairConnectDbContext db;

        public UserService(HairConnectDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await this.db
                .Users
                .ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await this.db
                .Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
