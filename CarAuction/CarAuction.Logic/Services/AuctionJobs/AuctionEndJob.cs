using CarAuction.Data.Enums;
using CarAuction.Data.Interfaces;
using Quartz;
using System.Linq;
using System.Threading.Tasks;

namespace CarAuction.Logic.Services.AuctionJobs
{
    [DisallowConcurrentExecution]
    public class AuctionEndJob : IJob
    {
        private readonly IAuctionRepository _auctionRepo;
        private readonly IBidRepository _bidRepo;

        public AuctionEndJob(IAuctionRepository auctionRepository, IBidRepository bidRepository)
        {
            _auctionRepo = auctionRepository;
            _bidRepo = bidRepository;
        }
        
        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var id = dataMap.GetInt("id");
            if (id == 0)
            {
                return;
            }
            var auction = await _auctionRepo.GetById(id);
            auction.Status = AuctionStatus.Completed;
            await _auctionRepo.Update(auction);

            var auctionInfo = await _auctionRepo.GetByIdWithCarsAndBids(id);

            // Define the winner for each lot in auction
            var winBids = auctionInfo.Assignments.Where(ac => ac.Bids.Any()).Select(ac =>
            {
                var winBid = ac.Bids.Aggregate((i1, i2) => i1.Amount > i2.Amount ? i1 : i2);
                winBid.WinResult = true;
                return winBid;                
            }).ToList();

            var tasks = winBids.Select(item => _bidRepo.Update(item)).ToList();
            await Task.WhenAll(tasks);                
        }
    }
}
