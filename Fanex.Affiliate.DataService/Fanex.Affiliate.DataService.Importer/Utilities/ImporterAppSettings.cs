namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    using System;
    using System.Configuration;
    using Fanex.Affiliate.DataService.Importer.Interface;

    public class ImporterAppSettings : IImporterAppSettings
    {
        public int CheckingTime1 => Convert.ToInt32("checkingTime1");

        public int CheckingTime2 => Convert.ToInt32(GetValue("checkingTime2"));

        public string DailyDataUrl => GetValue("DailyDataURL");

        public string FileExtension => GetValue("fileExtension");

        public int ForSystem => Convert.ToInt32(GetValue("forSystem"));

        public bool ForTest => GetValue("forTest") == "1";

        public string PathSaveFile => GetValue("pathSaveFile");

        public int AdminId => Convert.ToInt32(GetValue("adminID"));

        public string ToMail => GetValue("toMail");

        public string CcMail => GetValue("CCMail");

        public string BccMail => GetValue("BCCMail");

        public string SubjectMailError => GetValue("subjectMailError");

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}
