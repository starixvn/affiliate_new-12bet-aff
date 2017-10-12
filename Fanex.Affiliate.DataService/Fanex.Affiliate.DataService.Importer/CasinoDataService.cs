namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Xml;
    using Common.Enum;
    using Fanex.Affiliate.DataService.Importer.DTO;
    using Fanex.Affiliate.DataService.Importer.Enum;
    using Fanex.Affiliate.DataService.Importer.Repository;
    using Fanex.Affiliate.DataService.Importer.Utilities;

    public class CasinoDataService : ICasinoDataService
    {
        private readonly ICasinoDataRepository _casinoDataImporterRepository;
        private readonly IAppSettings _appSettings;
        private readonly ColumnName _columnName;

        public CasinoDataService() : this(null, null)
        {
            _columnName = new ColumnName();
        }

        public CasinoDataService(ICasinoDataRepository casinoDataImporterRepository, IAppSettings appSetting)
        {
            _casinoDataImporterRepository = casinoDataImporterRepository ?? new CasinoDataRepository();
            _appSettings = appSetting ?? new AppSettings();
            _columnName = new ColumnName(_appSettings);
        }

        public ResultInfo ImportData(DateType dateType)
        {
            var fileName = DownloadFile(dateType);
            var xmlDocument = ConvertCSVToXml(fileName);

            var metaModel = new CasinoDataMetaModel
            {
                DateImport = (dateType == DateType.Yesterday ? DateTime.Now.AddDays(-1) : DateTime.Now),
                XmlData = xmlDocument.OuterXml,
                AdminId = _appSettings.AdminId
            };

            var systemType = (SystemType)_appSettings.ForSystem;

            var result = _casinoDataImporterRepository.ImportCasinoData(systemType, metaModel);

            return result;
        }

        public string DownloadFile(DateType dateType)
        {
            var date = dateType == DateType.Yesterday ? DateTime.Now.AddDays(-1) : DateTime.Now;

            var path = $"{_appSettings.PathSaveFile}{date.ToString("yyyy/MM/")}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = $"{path}Casino {date.ToString("yyyyMMdd")}{_appSettings.FileExtension}";
            string downloadURL = _appSettings.DailyDataUrl.Replace("{date}", date.ToString("yyyy-MM-dd"));
            using (WebClient wc = new WebClient())
            {
                wc.Proxy = null;
                wc.DownloadFile(downloadURL, fileName);
            }

            return fileName;
        }

        public XmlDocument ConvertCSVToXml(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);

            StreamReader stream = new StreamReader(fileName);

            var hsColumnName = new Hashtable();
            string[] lineContent = stream.ReadLine().Split(',');
            for (int i = 0; i < lineContent.Length; i++)
            {
                if (!hsColumnName.ContainsKey(lineContent[i]))
                    hsColumnName.Add(lineContent[i], i);//Map order column to Column name
            }

            var indexDate = GetIndexOfColName(hsColumnName, _columnName.Date);
            var indexUsername = GetIndexOfColName(hsColumnName, _columnName.Username);
            var indexTurnover = GetIndexOfColName(hsColumnName, _columnName.Turnover);
            var indexDeposit = GetIndexOfColName(hsColumnName, _columnName.Deposit);
            var indexBonus = GetIndexOfColName(hsColumnName, _columnName.Bonus);
            var indexIAWinlost = GetIndexOfColName(hsColumnName, _columnName.IAWinlost);
            var indexWinlostAff = GetIndexOfColName(hsColumnName, _columnName.Winlost);

            while (!stream.EndOfStream)
            {
                lineContent = stream.ReadLine().Split(',');
                if (!string.IsNullOrEmpty(lineContent[GetIndexOfColName(hsColumnName, "Username")]))
                {
                    XmlElement transNode = doc.CreateElement("trans");
                    transNode.SetAttribute("date", lineContent[indexDate]);
                    transNode.SetAttribute("username", lineContent[indexUsername]);
                    transNode.SetAttribute("turnover", lineContent[indexTurnover]);
                    transNode.SetAttribute("deposit", lineContent[indexDeposit]);
                    transNode.SetAttribute("bonus", lineContent[indexBonus]);
                    transNode.SetAttribute("winlostaff", lineContent[indexWinlostAff]);
                    transNode.SetAttribute("winlost", lineContent[indexIAWinlost]);
                    root.AppendChild(transNode);
                }
            }
            stream.Close();
            return doc;
        }

        private static int GetIndexOfColName(Hashtable hs, string colName)
        {
            if (hs[colName] != null)
                return Convert.ToInt32(hs[colName]);
            else
                return Convert.ToInt32(hs["\"" + colName + "\""]);
        }
    }
}