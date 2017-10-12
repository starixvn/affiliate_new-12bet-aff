namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using Common.DTO;
    using Common.Ultilities;
    using Enum;
    using Utilities;

    public class CasinoDataImportingRunner : ICasinoDataImportingRunner
    {
        private const string LogTitle = "AFF Import Casino Data:";

        private readonly ICasinoDataService _casinoDataService;
        private readonly IAppSettings _appSettings;
        private readonly IDataServiceFactory _dataServiceFactory;

        public CasinoDataImportingRunner() : this(null, null, null)
        {
        }

        public CasinoDataImportingRunner(ICasinoDataService casinoDataService, IAppSettings appSetting, IDataServiceFactory dataServiceFactory)
        {
            _casinoDataService = casinoDataService ?? new CasinoDataService();
            _appSettings = appSetting ?? new AppSettings();
            _dataServiceFactory = dataServiceFactory ?? new DataServiceFactory();
        }

        protected IDataServiceLogger Logger => _dataServiceFactory.GetLogger();

        protected IEmailSender EmailSender => _dataServiceFactory.GetEmailSender();

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

                Logger.LogInfo($"{LogTitle} {logMessage}");

                if (!result.IsSuccessful)
                {
                    Logger.LogError($"{LogTitle} {result.Code}: {result.Message}");
                    SendMail(result.Message);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"{LogTitle} {ex.Message}", ex + "\r\nStacktrace: " + ex.StackTrace);
                SendMail(ex.Message);
            }
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