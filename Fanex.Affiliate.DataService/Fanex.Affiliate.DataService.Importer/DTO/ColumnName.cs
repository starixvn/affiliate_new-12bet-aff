using Fanex.Affiliate.DataService.Importer.Interface;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.Importer.DTO
{
    public class ColumnName
    {
        private readonly IImporterAppSettings _appSettings;

        public ColumnName() : this(null)
        {
        }

        public ColumnName(IImporterAppSettings appSetting)
        {
            _appSettings = appSetting ?? new ImporterAppSettings();
        }

        public string Date => _appSettings.GetValue("Date");

        public string Username => _appSettings.GetValue("Username");

        public string Turnover => _appSettings.GetValue("Turnover");

        public string Winlost => _appSettings.GetValue("Winlost");

        public string Deposit => _appSettings.GetValue("Deposit");

        public string Bonus => _appSettings.GetValue("Bonus");

        //IA column name
        public string IADate => _appSettings.GetValue("IADate");

        public string IAWinlost => _appSettings.GetValue("IAWinlost");

        public string IABonus => _appSettings.GetValue("IABonus");

        public string IADeposit => _appSettings.GetValue("IADeposit");
    }
}