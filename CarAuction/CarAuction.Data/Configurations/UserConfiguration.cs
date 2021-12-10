using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.Role)
                .IsRequired();
            builder.HasData(new User { Id = 1, UserName = "admin", Role = "admin", Password = "adm123" });
        }
    }
}
