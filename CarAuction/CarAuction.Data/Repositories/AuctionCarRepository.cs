using CarAuction.Data.Context;
using CarAuction.Data.Enums;
using CarAuction.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories
{
    internal class AuctionCarRepository : IAuctionCarRepository
    {
        private readonly ApplicationContext _db;

        public AuctionCarRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<int> GetAuctionCarPrice(int id)
        {
            return await _db.AuctionCars
                .Where(ac => ac.Id == id)
                .Select(ac => ac.AuctionPrice)
                .FirstOrDefaultAsync();
        }

        public async Task<AuctionStatus> GetAuctionStatus(int id)
        {
            return await _db.AuctionCars
                .Where(ac => ac.Id == id)
                .Select(ac => ac.Auction.Status)
                .FirstOrDefaultAsync(); 
        }
    }
}
