using Fanex.AffiliatePortal.Job.DataImporting.Configuration;

namespace Fanex.AffiliatePortal.Job.DataImporting.DTO
{
    public static class ColumnName
    {
        public static string Date => AppSettings.GetValue("Date");

        public static string Username => AppSettings.GetValue("Username");

        public static string Turnover => AppSettings.GetValue("Turnover");

        public static string Winlost => AppSettings.GetValue("Winlost");

        public static string Deposit => AppSettings.GetValue("Deposit");

        public static string Bonus => AppSettings.GetValue("Bonus");

        //IA column name
        public static string IADate => AppSettings.GetValue("IADate");

        public static string IAWinlost => AppSettings.GetValue("IAWinlost");

        public static string IABonus => AppSettings.GetValue("IABonus");

        public static string IADeposit => AppSettings.GetValue("IADeposit");
    }
}
