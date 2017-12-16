namespace HairConnect.Data.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
