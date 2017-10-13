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

    public class DailyMemberCheckSumRunner : IDailyMemberCheckSumRunner, IDisposable
    {
        private readonly IDailyMemberRepository _dailyMemberRepository;
        private readonly IEmailBuilder _emailBuilder;
        private readonly IAppSettings _appSettings;
        private readonly IMailDeliver _mailDeliver;
        private const string NotFoundError = "Not found discrepancy data";

        public DailyMemberCheckSumRunner() : this(null, null, null, null)
        {
        }

        public DailyMemberCheckSumRunner(
            IDailyMemberRepository dailyMemberRepository,
            IEmailBuilder emailBuilder,
            IAppSettings appSettings,
            IMailDeliver mailDeliver)
        {
            _appSettings = appSettings ?? new AppSettings();
            _dailyMemberRepository = dailyMemberRepository ?? new DailyMemberRepository();
            _emailBuilder = emailBuilder ?? new EmailBuilder();
            _mailDeliver = mailDeliver ?? new MailDeliver();
        }

        public void Run()
        {
            // decide time to compare data(check data 2 times: first at 11:00-12:00, second at 23:00-24:00)
            if (DateTime.Now.Hour == _appSettings.CheckingTime || _appSettings.ForTest)
            {
                var date = DateTime.Now.AddDays(_appSettings.ForDate);
                CheckDailyMember(date);
                CheckDailyMemberIncomeAccess(date);
            }
        }

        private void CheckDailyMember(DateTime date)
        {
            var result = _dailyMemberRepository.CheckSumDailyMemberFor12Bet(date);
            var emailContent = result.isMatch ? NotFoundError : _emailBuilder.GenerateDailyMemberContent(result);
            SendMail(emailContent);
        }

        private void CheckDailyMemberIncomeAccess(DateTime date)
        {
            var result = _dailyMemberRepository.CheckSumDailyMemberForIncomeAccess(date);
            var emailContent = result.isMatch ? NotFoundError : _emailBuilder.GenerateDailyMemberIncomeAccessContent(result);
            SendMail(emailContent);
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            if (_mailDeliver != null)
            {
                ((MailDeliver)_mailDeliver).Dispose();
            }
        }
    }
}