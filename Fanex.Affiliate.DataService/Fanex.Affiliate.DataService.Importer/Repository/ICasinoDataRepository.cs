namespace Fanex.Affiliate.DataService.Importer.Repository
{
    using Fanex.Affiliate.DataService.Importer.DTO;

    public interface ICasinoDataRepository
    {
        ResultInfo ImportDataFor12Bet(CasinoDataMetaModel metaModel);

        ResultInfo ImportDataForIncomeAccess(CasinoDataMetaModel metaModel);
    }
}