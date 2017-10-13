namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using DTO;

    public interface ICasinoDataService
    {
        ResultInfo ImportData(DateTime date);

        bool ImportData(string filePath);
    }
}