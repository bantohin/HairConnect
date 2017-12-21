namespace HairConnect.Services.Implementations
{
    using Data.Models;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class PictureService : IPictureService
    {
        private readonly HairConnectDbContext db;
        private readonly UserManager<User> userManager;

        public PictureService(HairConnectDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task CreatePicture(string id, IEnumerable<IFormFile> pictures)
        {
            foreach (IFormFile file in pictures)
            {
                Picture picture = new Picture()
                {
                    Content = this.PictureToByteArray(file),
                    UserId = id
                };

                await this.db.Pictures.AddAsync(picture);
            }

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAllPicturesFromUser(User user)
        {
            List<Picture> pictures = await this.db.Pictures.Where(p => p.UserId == user.Id).ToListAsync();
            this.db.RemoveRange(pictures);

            await this.db.SaveChangesAsync();
        }

        public async Task DeletePicture(int id)
        {
            Picture pic = await this.GetPictureById(id);
            this.db.Pictures.Remove(pic);
            await this.db.SaveChangesAsync();
        }

        public string DisplayPicture(byte[] picture)
        {
            if (picture != null)
            {
                string base64 = Convert.ToBase64String(picture);
                string imgSrc = String.Format("data:image/gif;base64,{0}", base64);

                return imgSrc;
            }

            return null;
        }

        public async Task<User> GetOwner(int id)
        {
            Picture pic = await this.GetPictureById(id);

            return pic.User;
        }

        public async Task<Picture> GetPictureById(int id)
        {
            return await this.db.Pictures.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool PictureExists(int id)
        {
            if (this.db.Pictures.Any(p => p.Id == id))
            {
                return true;
            }

            return false;
        }

        public byte[] PictureToByteArray(IFormFile picture)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                picture.CopyTo(stream);
                byte[] pictureBytes = stream.ToArray();

                return pictureBytes;
            }
        }
    }
}
