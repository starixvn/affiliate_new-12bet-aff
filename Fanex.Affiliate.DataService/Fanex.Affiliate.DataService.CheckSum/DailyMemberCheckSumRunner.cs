namespace Fanex.Affiliate.DataService.CheckSum
{
    using System;
    using Enum;
    using Fanex.Affiliate.DataService.CheckSum.Interface;
    using Logging;
    using MailDeliver.MailClient.API;
    using MailDeliver.Model;
    using Repository;
    using Utilities;

    public class DailyMemberCheckSumRunner : IDailyMemberCheckSumRunner
    {
        private readonly IDailyMemberRepository _dailyMemberRepository;
        private readonly IEmailContentBuilder _emailContentBuilder;
        private readonly IAppSettings _appSettings;
        private readonly ILogger _logger;
        private readonly IMailDeliver _mailDeliver;

        public DailyMemberCheckSumRunner() : this(null, null, null, null, null)
        {
        }

        public DailyMemberCheckSumRunner(
            IDailyMemberRepository dailyMemberRepository,
            IEmailContentBuilder emailContentBuilder,
            IAppSettings appSettings,
            ILogger logger,
            IMailDeliver mailDeliver)
        {
            _appSettings = appSettings ?? new AppSettings();
            _dailyMemberRepository = dailyMemberRepository ?? new DailyMemberRepository();
            _emailContentBuilder = emailContentBuilder ?? new EmailContentBuilder();
            _logger = logger ?? Logger.Log;
            _mailDeliver = mailDeliver ?? new MailDeliver();
        }

        public void Run()
        {
            // decide time to compare data(check data 2 times: first at 11:00-12:00, second at 23:00-24:00)
            if (DateTime.Now.Hour == _appSettings.CheckingTime || _appSettings.ForTest)
            {
                DateTime date = DateTime.Now.AddDays(_appSettings.ForDate);

                RunCheckSumBySystem(SystemType.Affiliate12Bet, date);
                RunCheckSumBySystem(SystemType.IncomeAccess, date);
            }
        }

        private void RunCheckSumBySystem(SystemType systemType, DateTime date)
        {
            bool isMatch = false;
            object dailyMemberData = null;
            var logTitle = string.Empty;

            switch (systemType)
            {
                case SystemType.Affiliate12Bet:
                    dailyMemberData = _dailyMemberRepository.CheckSumDailyMemberFor12Bet(date, out isMatch);
                    logTitle = "AffCheckSum run for date:";
                    break;

                case SystemType.IncomeAccess:
                    dailyMemberData = _dailyMemberRepository.CheckSumDailyMemberForIncomeAccess(date, out isMatch);
                    logTitle = "Income Access check sum run for date:";
                    break;
            }

            if (!isMatch)
            {
                var emailContent = _emailContentBuilder.Build(systemType, dailyMemberData, _appSettings.ForDate);
                SendMail(emailContent);
            }

            _logger.Info($"{logTitle} {DateTime.Now.AddDays(_appSettings.ForDate).ToShortDateString()}; IsMatch: {isMatch}");
        }

        private void SendMail(string content)
        {
            var title = _appSettings.SubjectMailError;

            var email = new MailRecord
            {
                MailTo = _appSettings.ToMail,
                Subject = title,
                Title = title,
                Body = content,
                MailBcc = _appSettings.BccMail,
                MailCc = _appSettings.CcMail
            };

            _mailDeliver.SendMail(email);
        }
    }
}