using CarAuction.Data.Configurations;
using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }
        
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<AuctionCar> AuctionCars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bid> Bids { get; set; }
                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuctionConfiguration).Assembly);
        }
    }
}
