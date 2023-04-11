using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Models.Users;

namespace SocialNetwork.Configuration
{
    public class FriendConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("Friends").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.User).WithMany();
            builder.HasOne(x => x.CurrentFriend).WithMany();
        }
    }
}
