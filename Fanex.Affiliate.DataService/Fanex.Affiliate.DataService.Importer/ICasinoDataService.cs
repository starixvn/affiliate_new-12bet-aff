namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using System.Xml;
    using DTO;

    public interface ICasinoDataService
    {
        ResultInfo ImportData(DateTime date);

        XmlDocument ConvertCSVToXml(string fileName);
    }
}