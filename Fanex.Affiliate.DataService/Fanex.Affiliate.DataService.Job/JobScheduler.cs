using System.Collections.Generic;
using Fanex.Affiliate.DataService.CheckSum;
using Fanex.Affiliate.DataService.Importer;
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

        public void Start(IDailyMemberCheckSumRunner dailyMemberCheckSumRunner, IDataImportingRunner dataImportingRunner)
        {
            var job = JobBuilder.Create<InHoursJob>()
                .UsingJobData(new JobDataMap(new Dictionary<string, IDailyMemberCheckSumRunner>
                {
                    { nameof(dailyMemberCheckSumRunner), dailyMemberCheckSumRunner}
                }))
                .UsingJobData(new JobDataMap(new Dictionary<string, IDataImportingRunner>
                {
                    { nameof(dataImportingRunner), dataImportingRunner}
                })).Build();

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