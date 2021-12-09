using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Data.Configurations
{
    public class AuctionCarConfiguration : IEntityTypeConfiguration<AuctionCar>
    {
        public void Configure(EntityTypeBuilder<AuctionCar> builder)
        {
            builder.HasKey(ac => new { ac.AuctionId, ac.CarId });

            builder.HasOne(ac => ac.Auction)
                .WithMany(a => a.Assignments)
                .HasForeignKey(ac => ac.AuctionId);

            builder.HasOne(ac => ac.Car)
                .WithMany(c => c.Assignments)
                .HasForeignKey(ac => ac.CarId);
        }
    }
}
