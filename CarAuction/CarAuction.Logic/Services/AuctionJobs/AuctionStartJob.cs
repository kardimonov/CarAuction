using CarAuction.Data.Enums;
using CarAuction.Data.Interfaces;
using Quartz;
using System.Threading.Tasks;

namespace CarAuction.Logic.Services.AuctionJobs
{
    [DisallowConcurrentExecution]
    public class AuctionStartJob : IJob
    {
        private readonly IAuctionRepository _repo;

        public AuctionStartJob(IAuctionRepository repository)
        {
            _repo = repository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var id = dataMap.GetInt("id");
            if (id == 0)
            {
                return;
            }
            var auction = await _repo.GetById(id);
            auction.Status = AuctionStatus.Started;
            await _repo.Update(auction);
        }
    }
}
