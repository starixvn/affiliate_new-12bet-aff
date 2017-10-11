using System;
using Fanex.AffiliatePortal.Job.DataImporting.Configuration;
using Fanex.AffiliatePortal.Job.DataImporting.Enum;
using Fanex.AffiliatePortal.Job.DataImporting.Service;
using Fanex.Logging;
using Fanex.MailDeliver.Model;

namespace Fanex.AffiliatePortal.Job.DataImporting
{
    public class CasinoDataImporting
    {
        private readonly DataImportingService _dataImportingService;

        public CasinoDataImporting(DataImportingService dataImportingService)
        {
            _dataImportingService = dataImportingService ?? new DataImportingService();
        }

        public void Run()
        {
            try
            {
                DateTime now = DateTime.Now;
                var dateImportType = DateImportType.Today;
                var logMessage = "Today - Last Time Run: " + now.ToLongDateString();

                //Run Yesterday Data at the beginning and the end of today
                if (now.Hour == AppSettings.CheckingTime1 || now.Hour == AppSettings.CheckingTime2 || AppSettings.ForTest)
                {
                    dateImportType = DateImportType.Yesterday;
                    logMessage = "Yesterday - Last Time Run: " + now.ToLongDateString();
                }

                var result = _dataImportingService.ImportData(dateImportType);

                LogService.Info("AFF Import Casino Data: " + logMessage);

                if (!result.IsSuccessful)
                {
                    LogService.Error("AFF Import Casino Data: " + result.Code + ": " + result.Message);
                    SendEmail(result.Message);
                }
            }
            catch (Exception ex)
            {
                LogService.Error("AFF Import Casino Data: " + ex.Message, ex + "\r\nStacktrace: " + ex.StackTrace);
                SendEmail(ex.Message);
            }
        }

        private void SendEmail(string content)
        {
            var mailRecord = new MailRecord
            {
                MailTo = AppSettings.ToMail,
                Subject = AppSettings.SubjectMailError,
                Title = AppSettings.SubjectMailError,
                Body = content, //IsBodyHtml ready
                MailBcc = AppSettings.BccMail,
                MailCc = AppSettings.CcMail
            };

            using (var client = new MailDeliver.MailClient.API.MailDeliver())
            {
                client.SendMail(mailRecord);
            }
        }
    }
}