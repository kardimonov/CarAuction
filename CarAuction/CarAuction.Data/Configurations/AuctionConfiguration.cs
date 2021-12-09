using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Data.Configurations
{
    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.StartTime)
                .IsRequired()
                .HasColumnType("datetime2(0)");
            builder.Property(e => e.EndTime)
                .IsRequired()
                .HasColumnType("datetime2(0)");
        }
    }
}
