namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using DTO;

    public interface IDataImportingRunner
    {
        ResultInfo Run();
    }
}