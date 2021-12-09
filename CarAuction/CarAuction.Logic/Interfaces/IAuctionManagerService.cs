using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuction.Logic.Interfaces
{
    public interface IAuctionManagerService : IService
    {
        Task ManageAuction(DateTime auctionStart, int id);
    }
}
