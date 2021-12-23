using CarAuction.Logic.Interfaces;
using CarAuction.Logic.Services.AuctionJobs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarAuction.Logic.Services
{
    public class AuctionManagerService : IAuctionManagerService
    {        
        private readonly ISchedulerFactory _factory;

        public AuctionManagerService(ISchedulerFactory factory)
        {
            _factory = factory;
        }

        public async Task StartAuction(DateTime time, int id)
        {
            var scheduler = await _factory.GetScheduler();

            var job = JobBuilder.Create<AuctionStartJob>()
                .WithIdentity($"job_{id}", "start")
                .UsingJobData("id", id)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"trigger_{id}", "start")
                .StartAt(time)
                .Build();
            
            await scheduler.ScheduleJob(job, trigger);
        }

        public async Task EndAuction(DateTime time, int id)
        {
            var scheduler = await _factory.GetScheduler();

            var job = JobBuilder.Create<AuctionEndJob>()
                .WithIdentity($"job_{id}", "end")
                .UsingJobData("id", id)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"trigger_{id}", "end")
                .StartAt(time)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public async Task RemoveAuctionJobs(int id)
        {
            var scheduler = await _factory.GetScheduler();

            var jobKeys = new List<JobKey>
            {
                new JobKey($"job_{id}", "start"),
                new JobKey($"job_{id}", "end")
            };
            
            await scheduler.DeleteJobs(jobKeys);
        }

        public async Task RescheduleAuctionStart(DateTime time, int id)
        {
            var scheduler = await _factory.GetScheduler();

            var newTrigger = TriggerBuilder.Create()
                .ForJob($"job_{id}", "start")
                .WithIdentity($"trigger_{id}", "start")
                .StartAt(time)
                .Build();

            var oldTriggerKey = new TriggerKey($"trigger_{id}", "start");
            await scheduler.RescheduleJob(oldTriggerKey, newTrigger);
        }
        
        public async Task RescheduleAuctionEnd(DateTime time, int id)
        {
            var scheduler = await _factory.GetScheduler();

            var newTrigger = TriggerBuilder.Create()
                .ForJob($"job_{id}", "end")
                .WithIdentity($"trigger_{id}", "end")
                .StartAt(time)
                .Build();

            var oldTriggerKey = new TriggerKey($"trigger_{id}", "end");
            await scheduler.RescheduleJob(oldTriggerKey, newTrigger);
        }
    }
}
