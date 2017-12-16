namespace HairConnect.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    
    public class HairConnectDbContext : IdentityDbContext<User>
    {
        public HairConnectDbContext(DbContextOptions<HairConnectDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Conversations 
            builder
                .Entity<Conversation>()
                .HasOne(c => c.Sender)
                .WithMany(s => s.ConversationsSent)
                .HasForeignKey(c => c.SenderId);

            builder
                .Entity<Conversation>()
                .HasOne(c => c.Receiver)
                .WithMany(r => r.ConversationsReceived)
                .HasForeignKey(c => c.ReceiverId);

            builder
                .Entity<Conversation>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId);

            //Reports
            builder
                .Entity<Report>()
                .HasKey(r => r.Id);

            builder
                .Entity<Report>()
                .HasOne(r => r.Sender)
                .WithMany(s => s.FiledReports)
                .HasForeignKey(r => r.SenderId);

            builder
                .Entity<Report>()
                .HasOne(r => r.ReportedUser)
                .WithMany(u => u.ReceivedReports)
                .HasForeignKey(r => r.ReportedUserId);

            //Messages
            builder
                .Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(s => s.Messages)
                .HasForeignKey(m => m.SenderId);

            builder
                .Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId);

            //Pictures
            builder
                .Entity<Picture>()
                .HasOne(p => p.User)
                .WithMany(u => u.Pictures)
                .HasForeignKey(p => p.UserId);

            base.OnModelCreating(builder);
        }
    }
}
