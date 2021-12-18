using CarAuction.Data.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Logic.Services.AuctionInfrastructure
{
    public class AuctionStarterJob : IJob
    {
        private readonly IAuctionRepository _repo;

        public AuctionStarterJob(IAuctionRepository repository)
        {
            _repo = repository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            int id = dataMap.GetInt("id");
            var auction = await _repo.GetById(id);
        }
    }
}
