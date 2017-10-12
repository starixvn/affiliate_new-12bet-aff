namespace Fanex.Affiliate.DataService.Importer
{
    using DTO;
    using Fanex.Affiliate.DataService.Importer.Enum;

    public interface ICasinoDataService
    {
        ResultInfo ImportData(DateType dateType);
    }
}