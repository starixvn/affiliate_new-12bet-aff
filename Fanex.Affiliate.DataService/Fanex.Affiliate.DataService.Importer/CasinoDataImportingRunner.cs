namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using Enum;
    using Logging;
    using MailDeliver.MailClient.API;
    using MailDeliver.Model;
    using Utilities;

    public class CasinoDataImportingRunner : ICasinoDataImportingRunner
    {
        private const string LogTitle = "AFF Import Casino Data:";

        private readonly ICasinoDataService _casinoDataService;
        private readonly IAppSettings _appSettings;
        private readonly ILogger _logger;
        private readonly IMailDeliver _mailDeliver;

        public CasinoDataImportingRunner() : this(null, null, null, null)
        {
        }

        public CasinoDataImportingRunner(ICasinoDataService casinoDataService, IAppSettings appSetting, ILogger logger, IMailDeliver mailDeliver)
        {
            _casinoDataService = casinoDataService ?? new CasinoDataService();
            _appSettings = appSetting ?? new AppSettings();
            _logger = logger ?? Logger.Log;
            _mailDeliver = mailDeliver ?? new MailDeliver();
        }

        public void Run()
        {
            try
            {
                DateTime now = DateTime.Now;
                var dateType = DateType.Today;
                var logMessage = "Today - Last Time Run: " + now.ToLongDateString();

                // Run Yesterday Data at the beginning and the end of today
                if (now.Hour == _appSettings.CheckingTime1 || now.Hour == _appSettings.CheckingTime2 || _appSettings.ForTest)
                {
                    dateType = DateType.Yesterday;
                    logMessage = "Yesterday - Last Time Run: " + now.ToLongDateString();
                }

                var result = _casinoDataService.ImportData(dateType);

                _logger.Info($"{LogTitle} {logMessage}");

                if (!result.IsSuccessful)
                {
                    _logger.Error($"{LogTitle} {result.Code}: {result.Message}");
                    SendMail(result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{LogTitle} {ex.Message}", ex + "\r\nStacktrace: " + ex.StackTrace);
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