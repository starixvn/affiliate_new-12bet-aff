namespace Fanex.Affiliate.DataService.Importer.Interface
{
    using Fanex.Affiliate.DataService.Importer.DTO;
    using Fanex.Affiliate.DataService.Importer.Enum;

    public interface ICasinoDataImporterRepository
    {
        ResultInfo ImportCasinoData(SystemType systemType, CasinoDataImporterMetaModel metaModel);
    }
}