using Quartz;
using Quartz.Impl;
using System;

namespace CarAuction.Logic.Services.AuctionInfrastructure
{
    public class AuctionScheduler
    {
        public static async void Start(DateTime time, int id)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<AuctionStarterJob>()
                .WithIdentity($"auction_start_{id}")
                .UsingJobData("id", id)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("trigger1", "group1")     // идентифицируем триггер с именем и группой
                //.WithSchedule(CronScheduleBuilder.)
                .StartAt(time)                            // запуск at Utc.time
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                    .WithRepeatCount(0))                   // 1 time
                .Build();                               // создаем триггер

            await scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
        }
    }
}
