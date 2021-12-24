using CarAuction.Data.Enums;
using System.Threading.Tasks;

namespace CarAuction.Data.Interfaces
{
    public interface IAuctionCarRepository : IRepository
    {
        Task<int> GetAuctionCarPrice(int id);
        Task<AuctionStatus> GetAuctionStatus(int id);
    }
}
