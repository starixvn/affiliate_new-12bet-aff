namespace Fanex.Affiliate.DataService.Importer.Repository
{
    using System.Data;
    using Fanex.Affiliate.DataService.Importer.DTO;
    using Fanex.Data;

    public class CasinoDataRepository : BaseRepository, ICasinoDataRepository
    {
        public ResultInfo ImportDataFor12Bet(CasinoDataMetaModel metaModel)
        {
            const string SpName = "Aff_CasinoImportData";

            using (var objectDb = GetObjectDb(SpName))
            {
                AssignCommonParam(objectDb, metaModel);
                objectDb.AddInParameter("importdate", DbType.DateTime, metaModel.DateImport);
                objectDb.AddInParameter("isreplacedata", DbType.Boolean, true);

                AddResultParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo ImportDataForIncomeAccess(CasinoDataMetaModel metaModel)
        {
            const string SpName = "Task_Aff_MemberDailyCasino_IA";

            using (var objectDb = GetObjectDb(SpName))
            {
                AssignCommonParam(objectDb, metaModel);
                objectDb.AddInParameter("winlostdate", DbType.Date, metaModel.DateImport);

                AddResultParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private void AssignCommonParam(IObjectDb objectDb, CasinoDataMetaModel metaModel)
        {
            objectDb.AddInParameter("adminid", DbType.Int32, metaModel.AdminId);
            objectDb.AddInParameter("xmldata", DbType.String, metaModel.XmlData);
        }
    }
}