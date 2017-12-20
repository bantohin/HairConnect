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
            List<User> users = await this.db
                .Users
                .ToListAsync();

            return users.OrderByDescending(u => u.Rating);
        }

        public async Task<User> GetUserById(string id)
        {
            return await this.db
                .Users
                .Where(u => u.Id == id)
                .Include(u => u.Pictures)
                .FirstOrDefaultAsync();
        }

        public async Task UpRating(string id)
        {
            User user = await this.GetUserById(id);
            user.Rating++;
            await this.db.SaveChangesAsync();
        }

        public async Task DownRating(string id)
        {
            User user = await this.GetUserById(id);
            user.Rating--;
            await this.db.SaveChangesAsync();
        }

        public async Task ChangeProfilePicture(string id, byte[] newPicture)
        {
            User user = await this.GetUserById(id);
            user.ProfilePicture = newPicture;
            await this.db.SaveChangesAsync();
        }
    }
}
