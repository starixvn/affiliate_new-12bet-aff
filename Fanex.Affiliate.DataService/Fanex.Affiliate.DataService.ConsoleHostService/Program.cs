using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.CheckSum;
using Fanex.Affiliate.DataService.Job;
using Fanex.Data.Configuration;
using Fanex.Logging;

namespace Fanex.Affiliate.DataService.ConsoleHostService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DbSettingProviderManager.Current.Start();
            IDailyMemberCheckSumRunner dailyMemberCheckSumRunner = new DailyMemberCheckSumRunner();
            var _scheduler = new JobScheduler();
            try
            {
                _scheduler.Start(dailyMemberCheckSumRunner);
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception.InnerException);
            }
        }
    }
}