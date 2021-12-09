using CarAuction.Data.Context;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return await _db.Auctions.FindAsync(id);
        }

        public async Task<List<Auction>> GetAll()
        {
            return await _db.Auctions.ToListAsync();
        }

        public async Task<int> Create(Auction auction)
        {
            _db.Auctions.Add(auction);
            await _db.SaveChangesAsync();
            return auction.Id;
        }

        public async Task Update(Auction auction)
        {
            _db.Auctions.Update(auction);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _db.Auctions.Remove(await _db.Auctions.FindAsync(id));
            await _db.SaveChangesAsync();
        }
    }
}
