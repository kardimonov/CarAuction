using CarAuction.Data.Enums;
using CarAuction.Data.Interfaces;
using CarAuction.Logic.Interfaces;
using System;
using System.Threading.Tasks;

namespace CarAuction.Logic.Services
{
    public class AuctionManagerService : IAuctionManagerService
    {
        private readonly IAuctionRepository _repo;

        public AuctionManagerService(IAuctionRepository repository)
        {
            _repo = repository;
        }

        public async Task ManageAuction(DateTime auctionStart, int id)
        {
            var timeSpan = auctionStart.Subtract(DateTime.UtcNow);
            await Task.Delay(timeSpan);

            var auction = await _repo.GetById(id);

            auction.Status = AuctionStatus.Opened;
            await _repo.Update(auction);

            Console.WriteLine("Medium");
            await Task.Delay(timeSpan);
            Console.WriteLine("End");
        }
    }
}
