namespace Fanex.Affiliate.DataService.CheckSum
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using Common.DTO;
    using Common.Enum;
    using Common.Ultilities;
    using Fanex.Affiliate.DataService.CheckSum.Interface;
    using Repository;
    using Utilities;

    public class DailyMemberCheckSumRunner : IDailyMemberCheckSumRunner
    {
        private readonly IAppSettings _appSettings;
        private readonly IDailyMemberRepository _dailyMemberRepository;
        private readonly IDataServiceFactory _dataServiceFactory;

        public DailyMemberCheckSumRunner() : this(null, null, null)
        {
        }

        public DailyMemberCheckSumRunner(
            IDailyMemberRepository dailyMemberRepository,
            IAppSettings appSettings,
            IDataServiceFactory dataServiceFactory)
        {
            _appSettings = appSettings ?? new AppSettings();
            _dailyMemberRepository = dailyMemberRepository ?? new DailyMemberRepository();
            _dataServiceFactory = dataServiceFactory ?? new DataServiceFactory();
        }

        protected IDataServiceLogger Logger => _dataServiceFactory.GetLogger();

        protected IEmailSender EmailSender => _dataServiceFactory.GetEmailSender();

        public void Run()
        {
            try
            {
                // decide time to compare data(check data 2 times: first at 11:00-12:00, second at 23:00-24:00)
                if (DateTime.Now.Hour == _appSettings.CheckingTime || _appSettings.ForTest)
                {
                    DateTime date = DateTime.Now.AddDays(_appSettings.ForDate);

                    RunCheckSumBySystem(SystemType.Affiliate12Bet, date);
                    RunCheckSumBySystem(SystemType.IncomeAccess, date);
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog("AFFCheckSum", ex);
                SendMail(ex.Message);
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

            if (!isMatch)
            {
                var emailContent = EmailContentBuilder.Build(systemType, dataTable, _appSettings.ForDate);
                SendMail(emailContent);
            }

            Logger.LogInfo($"{logTitle} {DateTime.Now.AddDays(_appSettings.ForDate).ToShortDateString()}; IsMatch: {isMatch}");
        }

        private void WriteErrorLog(string message, Exception exception)
        {
            var content = new StringBuilder().AppendLine(message).AppendLine(exception.Message);

            content.AppendLine("SOURCE: " + exception.Source)
                   .AppendLine("TARGETSITE: " + exception.TargetSite)
                   .AppendLine("STACKTRACE: " + exception.StackTrace);

            var sqlException = exception as SqlException;
            if (sqlException != null)
            {
                content.AppendLine("Procedure: " + sqlException.Procedure)
                    .AppendLine("Line Number: " + sqlException.LineNumber)
                    .AppendLine("Server: " + sqlException.Server);
            }

            Logger.LogError(content.ToString(), exception);
        }

        private void SendMail(string content)
        {
            var title = _appSettings.SubjectMailError;

            var email = new Email
            {
                To = _appSettings.ToMail,
                Subject = title,
                Title = title,
                Body = content,
                Bcc = _appSettings.BccMail,
                Cc = _appSettings.CcMail
            };

            EmailSender.Send(email);
        }
    }
}