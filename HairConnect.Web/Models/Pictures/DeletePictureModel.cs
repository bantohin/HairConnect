namespace HairConnect.Web.Models.Pictures
{
    using Common.Mapping;
    using Data.Models;

    public class DeletePictureModel : IMapFrom<Picture>
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }
    }
}
