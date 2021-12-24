using CarAuction.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarAuction.Data.Interfaces
{
    public interface IAuctionRepository : IRepository
    {
        Task<Auction> GetById(int id);
        Task<List<Auction>> GetAll();
        Task<Auction> GetByIdWithCarsAndBids(int id);
        Task<int> Create(Auction auction);
        Task Update(Auction auction);
        Task Delete(int id);
    }
}
