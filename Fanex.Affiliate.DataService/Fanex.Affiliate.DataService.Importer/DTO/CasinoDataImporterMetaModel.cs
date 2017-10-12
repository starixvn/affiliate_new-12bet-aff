namespace Fanex.Affiliate.DataService.Importer.DTO
{
    using System;

    public class CasinoDataImporterMetaModel
    {
        public int AdminId { get; set; }

        public string XmlData { get; set; }

        public DateTime DateImport { get; set; }
    }
}