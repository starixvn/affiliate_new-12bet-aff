using Fanex.Affiliate.DataService.CheckSum.Interface;
using Quartz;

namespace Fanex.Affiliate.DataService.Job
{
    public class DailyCheckSum : IJob
    {
        private readonly IDailyMemberCheckSumRunner _sumService;

        public DailyCheckSum(IDailyMemberCheckSumRunner sumService)
        {
            _sumService = sumService;
        }

        public void Execute(IJobExecutionContext context)
        {
            _sumService.Run();
        }
    }
}