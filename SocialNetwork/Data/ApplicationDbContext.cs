using System;
using System.Collections.Generic;
using System.Text;
using SocialNetwork.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Configuration;

namespace SocialNetwork.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(u => u.NormalizedEmail).IsUnique();
            });

            modelBuilder.ApplyConfiguration(new FriendConfiguration());
        }
    }
}
