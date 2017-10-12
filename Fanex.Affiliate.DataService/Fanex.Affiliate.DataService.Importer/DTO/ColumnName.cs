namespace Fanex.Affiliate.DataService.Importer.DTO
{
    using Fanex.Affiliate.DataService.Importer.Utilities;

    public class ColumnName
    {
        private readonly IAppSettings _appSettings;

        public ColumnName() : this(null)
        {
        }

        public ColumnName(IAppSettings appSetting)
        {
            _appSettings = appSetting ?? new AppSettings();
        }

        public string Date => _appSettings.GetValue("Date");

        public string Username => _appSettings.GetValue("Username");

        public string Turnover => _appSettings.GetValue("Turnover");

        public string Winlost => _appSettings.GetValue("Winlost");

        public string Deposit => _appSettings.GetValue("Deposit");

        public string Bonus => _appSettings.GetValue("Bonus");

        public string IADate => _appSettings.GetValue("IADate");

        public string IAWinlost => _appSettings.GetValue("IAWinlost");

        public string IABonus => _appSettings.GetValue("IABonus");

        public string IADeposit => _appSettings.GetValue("IADeposit");
    }
}