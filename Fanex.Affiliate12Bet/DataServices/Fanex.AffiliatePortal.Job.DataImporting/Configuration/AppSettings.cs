using System;
using System.Configuration;

namespace Fanex.AffiliatePortal.Job.DataImporting.Configuration
{
    public static class AppSettings
    {
        public static int CheckingTime1 => Convert.ToInt32("checkingTime1");

        public static int CheckingTime2 => Convert.ToInt32(GetValue("checkingTime2"));

        public static string DailyDataUrl => GetValue("DailyDataURL");

        public static string FileExtension => GetValue("fileExtension");

        public static int ForSystem => Convert.ToInt32(GetValue("forSystem"));

        public static bool ForTest => GetValue("forTest") == "1";

        public static string PathSaveFile => GetValue("pathSaveFile");

        public static int AdminId => Convert.ToInt32(GetValue("adminID"));

        public static string ToMail => GetValue("toMail");

        public static string CcMail => GetValue("CCMail");

        public static string BccMail => GetValue("BCCMail");

        public static string SubjectMailError => GetValue("subjectMailError");

        public static string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}