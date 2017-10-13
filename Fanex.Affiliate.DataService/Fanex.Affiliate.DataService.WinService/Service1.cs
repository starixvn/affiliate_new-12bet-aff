using System;
using System.ServiceProcess;
using Fanex.Affiliate.DataService.CheckSum;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Job;
using Fanex.Logging;

namespace Fanex.Affiliate.DataService.WinService
{
    public partial class Service1 : ServiceBase
    {
        private readonly IDailyMemberCheckSumRunner _dailyMemberCheckSumRunner;
        private readonly JobScheduler _scheduler;
        private readonly IDataImportingRunner _dataImportingRunner;

        public Service1()
        {
            InitializeComponent();
            _scheduler = new JobScheduler();
            _dailyMemberCheckSumRunner = new DailyMemberCheckSumRunner();
            _dataImportingRunner = new DataImportingRunner();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _scheduler.Start(_dailyMemberCheckSumRunner, _dataImportingRunner);
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