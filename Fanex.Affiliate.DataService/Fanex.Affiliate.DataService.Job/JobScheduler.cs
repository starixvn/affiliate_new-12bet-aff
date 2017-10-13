using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.CheckSum;
using Quartz;
using Quartz.Impl;

namespace Fanex.Affiliate.DataService.Job
{
    public class JobScheduler
    {
        private readonly IScheduler _scheduler;

        public JobScheduler()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        public void Start(IDailyMemberCheckSumRunner dailyMemberCheckSumRunner)
        {
            var job = JobBuilder.Create<InHoursJob>()
                .UsingJobData(new JobDataMap(new Dictionary<string, IDailyMemberCheckSumRunner>
                {
                    { nameof(dailyMemberCheckSumRunner), dailyMemberCheckSumRunner}
                }))
                .Build();

            var trigger = TriggerBuilder.Create()
                   .WithSimpleSchedule(a => a.WithIntervalInHours(1)
                   .RepeatForever())
                   .Build();

            _scheduler.ScheduleJob(job, trigger);
            _scheduler.Start();
        }

        public void Stop()
        {
            _scheduler.Shutdown();
        }
    }
}