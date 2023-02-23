using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Quartz;
using Quartz.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskSchedular.Models;

namespace TaskSchedular.CronJob
{
    public class CronTaskHandler
    {
        public CronTaskHandler() { }
        private int CalculateInerval(int noJobs)
        {
            var timeFor1JobInHours = 1.0 / noJobs;
            var timeInSeconds = timeFor1JobInHours * 3600;
            return Convert.ToInt32(timeInSeconds);
        }

        public async Task  Run() {

            await Console.Out.WriteAsync("\n CronJob Background service running \n");
            var db = new AppDBContext();
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();
            await scheduler.Clear();

            var compeigns = db.Campaigns.ToList();

            foreach (var compeign in compeigns )
            {
                var interval = CalculateInerval(Convert.ToInt32(compeign.deliverythrottle));
                var jobId = "JOB:"+compeign.Id+"_INTERVAL:"+ interval.ToString()+" Sec";
                var triggerId = "TRIGGER_" + compeign.Id + "_" + Guid.NewGuid().ToString();

                IJobDetail job = JobBuilder.Create<LeadSender>()
                  .WithIdentity(jobId, "group1")
                  .UsingJobData("CompaignData",JsonSerializer.Serialize(compeign))
                  .StoreDurably(true)
                  .Build();

                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerId, "group1")
                .StartNow()
                .ForJob(job)
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(interval))
                .Build();

                await scheduler.ScheduleJob(job, trigger);

            }

             ((IDisposable)db).Dispose();

        }
    }

}
