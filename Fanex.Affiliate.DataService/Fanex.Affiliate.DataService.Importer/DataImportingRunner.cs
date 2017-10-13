namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using DTO;
    using Utilities;

    public class DataImportingRunner : IDataImportingRunner
    {
        private readonly ICasinoDataService _casinoDataService;
        private readonly IAppSettings _appSettings;

        public DataImportingRunner() : this(null, null)
        {
        }

        public DataImportingRunner(ICasinoDataService casinoDataService, IAppSettings appSetting)
        {
            _casinoDataService = casinoDataService ?? new CasinoDataService();
            _appSettings = appSetting ?? new AppSettings();
        }

        public ResultInfo Run()
        {
            var date = DateTime.Now;

            // Run Yesterday Data at the beginning and the end of today
            if (date.Hour == _appSettings.CheckingTime1 || date.Hour == _appSettings.CheckingTime2 || _appSettings.ForTest)
            {
                date = date.AddDays(-1);
            }

            return _casinoDataService.ImportData(date);
        }
    }
}