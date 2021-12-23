using CarAuction.Data.Context;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories
{
    internal class AuctionRepository : IAuctionRepository
    {
        private readonly ApplicationContext _db;

        public AuctionRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<Auction> GetById(int id)
        {
            return await _db.Auctions.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Auction>> GetAll()
        {
            return await _db.Auctions.AsNoTracking().ToListAsync();
        }

        public async Task<Auction> GetByIdWithCarsAndBids(int id)
        {
            return await _db.Auctions
                .Include(a => a.Assignments)
                .ThenInclude(ac => ac.Car)
                .Include(a => a.Assignments)
                .ThenInclude(ac => ac.Bids)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<int> Create(Auction auction)
        {
            _db.Auctions.Add(auction);
            await _db.SaveChangesAsync();
            return auction.Id;
        }

        public async Task Update(Auction auction)
        {
            _db.Attach(auction);
            _db.Entry(auction).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _db.Auctions.Remove(await _db.Auctions.FindAsync(id));
            await _db.SaveChangesAsync();
        }

        public async Task<Auction> GetByAuctionCarId(int auctionCarId)
        {
            return await _db.Auctions
                .Where(a => a.Assignments.Any(ac => ac.Id == auctionCarId))
                .FirstOrDefaultAsync();
        }
    }
}
