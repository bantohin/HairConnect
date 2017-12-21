namespace HairConnect.Services.Interfaces
{
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPictureService
    {
        byte[] PictureToByteArray(IFormFile picture);

        string DisplayPicture(byte[] picture);

        Task CreatePicture(string id, IEnumerable<IFormFile> pictures);

        bool PictureExists(int id);

        Task<User> GetOwner(int id);

        Task<Picture> GetPictureById(int id);

        Task DeletePicture(int id);

        Task DeleteAllPicturesFromUser(User user);
    }
}
