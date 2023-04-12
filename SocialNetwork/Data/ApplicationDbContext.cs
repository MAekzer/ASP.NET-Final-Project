using SocialNetwork.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace SocialNetwork.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(u => u.NormalizedEmail).IsUnique();
            });

            modelBuilder.Entity<Friend>().ToTable("Friends").HasKey(p => p.Id);
            modelBuilder.Entity<Friend>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Friend>().HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Friend>().HasOne(x => x.CurrentFriend).WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>().ToTable("Messages").HasKey(p => p.Id);
            modelBuilder.Entity<Message>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Message>().HasOne(x => x.Sender).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>().HasOne(x => x.Recipient).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
