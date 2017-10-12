namespace Fanex.Affiliate.DataService.CheckSum
{
    using System;
    using System.Data;
    using Enum;
    using Fanex.Affiliate.DataService.CheckSum.Interface;
    using Logging;
    using Repository;
    using Utilities;

    public class DailyMemberCheckSumRunner : IDailyMemberCheckSumRunner
    {
        private readonly ICheckSumAppSettings _appSettings;
        private readonly IDailyMemberRepository _dailyMemberRepository;

        public DailyMemberCheckSumRunner() : this(null, null)
        {
        }

        public DailyMemberCheckSumRunner(IDailyMemberRepository dailyMemberRepository, ICheckSumAppSettings appSettings)
        {
            _appSettings = appSettings ?? new CheckSumAppSettings();
            _dailyMemberRepository = dailyMemberRepository ?? new DailyMemberRepository();
        }

        public void Run()
        {
            try
            {
                //decide time to compare data(check data 2 times: first at 11:00-12:00, second at 23:00-24:00)
                if (DateTime.Now.Hour == _appSettings.CheckingTime || _appSettings.ForTest)
                {
                    DateTime date = DateTime.Now.AddDays(_appSettings.ForDate);

                    RunCheckSumBySystem(SystemType.Affiliate12Bet, date);
                    RunCheckSumBySystem(SystemType.IncomeAccess, date);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog("AFFCheckSum", ex);
                //SendEmail(_appSettings.SubjectMailError, ex.Message);
            }
        }

        private void RunCheckSumBySystem(SystemType systemType, DateTime date)
        {
            bool isMatch = false;
            DataTable dataTable = null;
            var logTitle = string.Empty;

            switch (systemType)
            {
                case SystemType.Affiliate12Bet:
                    dataTable = _dailyMemberRepository.CheckSumDailyMemberFor12Bet(date, out isMatch);
                    logTitle = "AffCheckSum run for date:";
                    break;

                case SystemType.IncomeAccess:
                    dataTable = _dailyMemberRepository.CheckSumDailyMemberForIncomeAccess(date, out isMatch);
                    logTitle = "Income Access check sum run for date:";
                    break;
            }

            //if failed: send email to IT team
            if (!isMatch)
            {
                //SendEmail(_appSettings.SubjectMailError, buildBodyEmail(rs.ResultSet.Tables[0]));
            }

            LogService.Info($"{logTitle} {DateTime.Now.AddDays(_appSettings.ForDate).ToShortDateString()}; IsMatch: {isMatch}");
        }
    }
}