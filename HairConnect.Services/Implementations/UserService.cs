namespace HairConnect.Services.Implementations
{
    using System.Collections.Generic;
    using Interfaces;
    using Data;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Data.Models;
    using System.Linq;
    using Models.Users;
    using Microsoft.AspNetCore.Identity;
    using AutoMapper;

    public class UserService : IUserService
    {
        private readonly HairConnectDbContext db;
        private readonly UserManager<User> userManager;

        public UserService(HairConnectDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
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

        public async Task<IEnumerable<UserListingModel>> GetUsersToList()
        {
            List<User> users = new List<User>();

            foreach (User profile in await this.GetAllUsers())
            {
                IEnumerable<string> roles = await this.userManager.GetRolesAsync(profile);
                if (roles.Contains(ServicesConstants.HairdresserRole))
                {
                    users.Add(profile);
                }
            }

            IEnumerable<UserListingModel> profiles = users.Select(Mapper.Map<User, UserListingModel>);

            return profiles;
        }

        public UserDetailsModel GetUserDetails(User user)
        {
            return Mapper.Map<UserDetailsModel>(user);
        }

        public async Task<IEnumerable<ListUsersModel>> GetUsersToListAdmin()
        {
            List<ListUsersModel> users = Mapper.Map<List<ListUsersModel>>(await this.GetAllUsers());
            foreach (ListUsersModel user in users)
            {
                user.Roles = this.userManager.GetRolesAsync(await this.userManager.FindByIdAsync(user.Id)).Result.ToList();
            }

            return users;
        }
    }
}
