﻿namespace HairConnect.Services.Interfaces
{
    using Data.Models;
    using Models.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(string id);

        Task ChangeProfilePicture(string id, byte[] newPicture);

        Task UpRating(string id);

        Task DownRating(string id);

        Task<IEnumerable<UserListingModel>> GetUsersToList();

        UserDetailsModel GetUserDetails(User user);

        Task<IEnumerable<ListUsersModel>> GetUsersToListAdmin();
    }

}
