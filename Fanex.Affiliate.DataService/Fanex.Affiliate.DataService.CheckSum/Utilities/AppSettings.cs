namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;
    using System.Configuration;

    public class AppSettings : IAppSettings
    {
        public int CheckingTime => Convert.ToInt32(GetValue("checkingTime"));

        public bool ForTest => Convert.ToBoolean(GetValue("forTest"));

        public int ForDate => Convert.ToInt32(GetValue("forDate"));

        public string ToMail => GetValue("toMail");

        public string CcMail => GetValue("CCMail");

        public string BccMail => GetValue("BCCMail");

        public string SubjectMailError => GetValue("subjectMailError");

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}
