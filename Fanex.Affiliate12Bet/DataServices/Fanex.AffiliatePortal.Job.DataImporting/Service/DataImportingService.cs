using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Xml;
using Fanex.AffiliatePortal.Job.DataImporting.Configuration;
using Fanex.AffiliatePortal.Job.DataImporting.DataAccess;
using Fanex.AffiliatePortal.Job.DataImporting.DTO;
using Fanex.AffiliatePortal.Job.DataImporting.Enum;
using Fanex.AffiliatePortal.Job.DataImporting.MetaModel;
using Fanex.AffiliatePortal.Job.DTO;

namespace Fanex.AffiliatePortal.Job.DataImporting.Service
{
    public class DataImportingService
    {
        private readonly CasinoDataImportingDataAccess _casinoDataImportingDataAccess;

        public DataImportingService() : this(null)
        {
        }

        public DataImportingService(CasinoDataImportingDataAccess casinoDataImportingDataAccess)
        {
            _casinoDataImportingDataAccess = casinoDataImportingDataAccess ?? new CasinoDataImportingDataAccess();
        }

        public JobResult ImportData(DateImportType dateImport)
        {
            var fileName = DownloadFile(dateImport);
            var xmlDocument = ConvertCSVToXml(fileName);

            var metaModel = new CasinoDataImportingMetaModel
            {
                DateImport = (dateImport == DateImportType.Yesterday ? DateTime.Now.AddDays(-1) : DateTime.Now),
                XmlData = xmlDocument.OuterXml,
                AdminId = AppSettings.AdminId
            };

            SystemType systemType = (SystemType)AppSettings.ForSystem;

            var result = _casinoDataImportingDataAccess.ImportCasinoData(systemType, metaModel);

            return result;
        }

        public string DownloadFile(DateImportType dateImport)
        {
            var date = dateImport == DateImportType.Yesterday ? DateTime.Now.AddDays(-1) : DateTime.Now;

            var path = $"{AppSettings.PathSaveFile}{date.ToString("yyyy/MM/")}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = $"{path}Casino {date.ToString("yyyyMMdd")}{AppSettings.FileExtension}";
            string downloadURL = AppSettings.DailyDataUrl.Replace("{date}", date.ToString("yyyy-MM-dd"));
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

            Hashtable hsColumnName = new Hashtable();
            string[] lineContent = stream.ReadLine().Split(',');
            for (int i = 0; i < lineContent.Length; i++)
            {
                if (!hsColumnName.ContainsKey(lineContent[i]))
                    hsColumnName.Add(lineContent[i], i);//Map order column to Column name
            }

            var indexDate = GetIndexOfColName(hsColumnName, ColumnName.Date);
            var indexUsername = GetIndexOfColName(hsColumnName, ColumnName.Username);
            var indexTurnover = GetIndexOfColName(hsColumnName, ColumnName.Turnover);
            var indexDeposit = GetIndexOfColName(hsColumnName, ColumnName.Deposit);
            var indexBonus = GetIndexOfColName(hsColumnName, ColumnName.Bonus);
            var indexIAWinlost = GetIndexOfColName(hsColumnName, ColumnName.IAWinlost);
            var indexWinlostAff = GetIndexOfColName(hsColumnName, ColumnName.Winlost);

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