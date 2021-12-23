using System;
using System.Threading.Tasks;

namespace CarAuction.Logic.Interfaces
{
    public interface IAuctionManagerService : IService
    {
        Task StartAuction(DateTime time, int id);
        Task EndAuction(DateTime time, int id);
        Task RemoveAuctionJobs(int id);
        Task RescheduleAuctionStart(DateTime time, int id);
        Task RescheduleAuctionEnd(DateTime time, int id);
    }
}
