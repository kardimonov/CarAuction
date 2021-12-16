using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Data.Configurations
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasOne(b => b.AuctionCar)
                .WithMany(ac => ac.Bids)
                .HasForeignKey(b => b.AuctionCarId);
                        
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bids)
                .HasForeignKey(b => b.UserId);

            builder.Property(b => b.Amount)
                .IsRequired();
            builder.Property(b => b.Time)
                .IsRequired();
            builder.Property(b => b.WinResult)
                .IsRequired();
        }
    }
}
