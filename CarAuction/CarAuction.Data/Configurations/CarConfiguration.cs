using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Data.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(c => c.Manufacture)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.VIN)
                .IsRequired()
                .HasMaxLength(17);
            builder.Property(c => c.Odometer)
                .IsRequired();
            builder.Property(c => c.Year)
                .IsRequired();
            builder.Property(c => c.ExteriorColor)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.InteriorColor)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.StrongScratches)
                .IsRequired();
            builder.Property(c => c.SmallScratches)
                .IsRequired();
            builder.Property(c => c.SuspensionProblems)
                .IsRequired();
            builder.Property(c => c.ElectricsFailures)
                .IsRequired();
            builder.Property(c => c.MSRPrice)
                .IsRequired();
        }
    }
}
