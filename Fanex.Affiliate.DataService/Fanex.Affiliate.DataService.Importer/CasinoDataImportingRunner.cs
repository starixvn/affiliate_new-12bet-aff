namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using Enum;
    using MailDeliver.MailClient.API;
    using MailDeliver.Model;
    using Utilities;

    public class CasinoDataImportingRunner : ICasinoDataImportingRunner
    {
        private readonly ICasinoDataService _casinoDataService;
        private readonly IAppSettings _appSettings;
        private readonly IMailDeliver _mailDeliver;

        public CasinoDataImportingRunner() : this(null, null, null)
        {
        }

        public CasinoDataImportingRunner(ICasinoDataService casinoDataService, IAppSettings appSetting, IMailDeliver mailDeliver)
        {
            _casinoDataService = casinoDataService ?? new CasinoDataService();
            _appSettings = appSetting ?? new AppSettings();
            _mailDeliver = mailDeliver ?? new MailDeliver();
        }

        public void Run()
        {
            try
            {
                DateTime now = DateTime.Now;
                var dateType = DateType.Today;

                // Run Yesterday Data at the beginning and the end of today
                if (now.Hour == _appSettings.CheckingTime1 || now.Hour == _appSettings.CheckingTime2 || _appSettings.ForTest)
                {
                    dateType = DateType.Yesterday;
                }

                var result = _casinoDataService.ImportData(dateType);

                if (!result.IsSuccessful)
                {
                    SendMail(result.Message);
                }
            }
            catch (Exception ex)
            {
                SendMail(ex.Message);
            }
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