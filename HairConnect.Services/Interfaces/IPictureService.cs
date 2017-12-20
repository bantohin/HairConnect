namespace HairConnect.Services.Interfaces
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPictureService
    {
        byte[] PictureToByteArray(IFormFile picture);

        string DisplayPicture(byte[] picture);

        Task CreatePicture(string id, IEnumerable<IFormFile> pictures);
    }
}
