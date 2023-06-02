using ChatWebApi.Models.Member;
using ChatWebApi.Models.Message;
using Microsoft.EntityFrameworkCore;

namespace ChatWebApi.Context
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<MemberEntity> Members { get; set; }

        public DbSet<MessageEntity> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MemberEntity>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<MemberEntity>()
                .HasIndex(x => x.Email)
                .IsUnique();

            builder.Entity<MemberEntity>()
                .HasMany(x => x.Messages)
                .WithOne(x => x.Member)
                .HasForeignKey(x => x.FromId)
                .IsRequired();

            builder.Entity<MessageEntity>()
                .HasOne(x => x.Member)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.FromId)
                .IsRequired();

            builder.Entity<MessageEntity>()
                .Property(x => x.SendDateTime)
                .HasDefaultValueSql("getdate()");

        }
    }
}
