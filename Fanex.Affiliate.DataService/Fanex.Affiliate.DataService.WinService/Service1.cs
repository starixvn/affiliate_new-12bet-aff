using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.CheckSum;
using Fanex.Affiliate.DataService.Job;
using Fanex.Logging;
using Quartz;

namespace Fanex.Affiliate.DataService.WinService
{
    public partial class Service1 : ServiceBase
    {
        private readonly IDailyMemberCheckSumRunner _dailyMemberCheckSumRunner;
        private readonly JobScheduler _scheduler;

        public Service1()
        {
            InitializeComponent();
            _scheduler = new JobScheduler();
            _dailyMemberCheckSumRunner = new DailyMemberCheckSumRunner();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _scheduler.Start(_dailyMemberCheckSumRunner);
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception.InnerException);
            }
        }

        protected override void OnStop()
        {
            try
            {
                _scheduler.Stop();
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception.InnerException);
            }
        }
    }
}