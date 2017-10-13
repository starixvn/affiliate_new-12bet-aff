namespace Fanex.Affiliate.DataService.Importer.Repository
{
    using Enum;
    using Fanex.Affiliate.DataService.Importer.DTO;

    public interface ICasinoDataRepository
    {
        ResultInfo ImportCasinoData(SystemType systemType, CasinoDataMetaModel metaModel);
    }
}