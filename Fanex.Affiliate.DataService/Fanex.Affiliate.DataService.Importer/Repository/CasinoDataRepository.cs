namespace Fanex.Affiliate.DataService.Importer.Repository
{
    using System.Data;
    using Common.Enum;
    using Fanex.Affiliate.DataService.Importer.DTO;
    using Fanex.Data;

    public class CasinoDataRepository : BaseRepository, ICasinoDataRepository
    {
        public ResultInfo ImportCasinoData(SystemType systemType, CasinoDataMetaModel metaModel)
        {
            switch (systemType)
            {
                case SystemType.All:
                    var result = ImportDataFor12Bet(metaModel);

                    if (result.IsSuccessful)
                    {
                        return ImportDataForIncomeAccess(metaModel);
                    }

                    return result;

                case SystemType.Affiliate12Bet:
                    return ImportDataFor12Bet(metaModel);

                case SystemType.IncomeAccess:
                    return ImportDataForIncomeAccess(metaModel);

                default:
                    return new ResultInfo { Code = 999, Message = "System Type is not defined" };
            }
        }

        private ResultInfo ImportDataFor12Bet(CasinoDataMetaModel metaModel)
        {
            const string SpName = "Aff_CasinoImportData";

            using (var objectDb = new ObjectDb(SpName))
            {
                AssignCommonParam(objectDb, metaModel);
                objectDb.AddInParameter("importdate", DbType.DateTime, metaModel.DateImport);
                objectDb.AddInParameter("isreplacedata", DbType.Boolean, true);

                AddResultParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private ResultInfo ImportDataForIncomeAccess(CasinoDataMetaModel metaModel)
        {
            const string SpName = "Task_Aff_MemberDailyCasino_IA";

            using (var objectDb = new ObjectDb(SpName))
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