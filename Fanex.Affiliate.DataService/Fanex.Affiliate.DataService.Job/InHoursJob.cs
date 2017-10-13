using System;
using Fanex.Affiliate.DataService.CheckSum;
using Fanex.Logging;
using Quartz;

namespace Fanex.Affiliate.DataService.Job
{
    public class InHoursJob : IJob
    {
        public InHoursJob()
        {
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var dataMap = context.JobDetail.JobDataMap;
                var sumService = (IDailyMemberCheckSumRunner)dataMap.Get("dailyMemberCheckSumRunner");

                sumService.Run();
            }
            catch (Exception exception)
            {
                LogService.Error(GetExceptionMessage(exception), exception.InnerException);
            }
        }

        private static string GetExceptionMessage(Exception exception)
        {
            var messages = new string[]
            {
                "ERROR: " + exception.Message,
                "SOURCE: " + exception.Source,
                "TARGETSITE: " + exception.TargetSite,
                "STACKTRACE: " + exception.StackTrace
            };

            return string.Join(Environment.NewLine, messages);
        }
    }
}