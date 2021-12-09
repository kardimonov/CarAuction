using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Data.Configurations
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasOne(b => b.Auction)
                .WithMany(a => a.Bids)
                .HasForeignKey(b => b.AuctionId);

            builder.HasOne(b => b.Car)
                .WithMany(c => c.Bids)
                .HasForeignKey(b => b.CarId);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Bids)
                .HasForeignKey(b => b.UserId);

            builder.Property(b => b.Amount)
                .IsRequired();
            builder.Property(b => b.WinResult)
                .IsRequired();
        }
    }
}
