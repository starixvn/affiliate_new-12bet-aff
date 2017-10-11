using System;
using System.Configuration;

namespace Fanex.AffiliatePortal.Job.CheckSum.Configuration
{
    public static class AppSettings
    {
        public static int CheckingTime => Convert.ToInt32(GetValue("checkingTime"));

        public static bool ForTest => Convert.ToBoolean(GetValue("forTest"));

        public static int ForDate => Convert.ToInt32(GetValue("forDate"));

        public static string ToMail => GetValue("toMail");

        public static string CcMail => GetValue("CCMail");

        public static string BccMail => GetValue("BCCMail");

        public static string SubjectMailError => GetValue("subjectMailError");

        public static string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}