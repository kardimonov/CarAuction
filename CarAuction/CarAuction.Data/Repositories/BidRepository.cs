using CarAuction.Data.Context;
using CarAuction.Data.Interfaces;
using CarAuction.Data.Models;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories
{
    internal class BidRepository : IBidRepository
    {
        private readonly ApplicationContext _db;

        public BidRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task Create(Bid bid)
        {
            _db.Bids.Add(bid);
            await _db.SaveChangesAsync();
        }
    }
}
