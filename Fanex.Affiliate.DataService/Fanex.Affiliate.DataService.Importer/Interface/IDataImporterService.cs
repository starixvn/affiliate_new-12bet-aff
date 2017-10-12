namespace Fanex.Affiliate.DataService.Importer.Interface
{
    using DTO;
    using Fanex.Affiliate.DataService.Importer.Enum;

    public interface IDataImporterService
    {
        ResultInfo ImportData(DateImportType dateImport);
    }
}