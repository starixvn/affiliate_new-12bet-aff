namespace Fanex.Affiliate.DataService.Importer.Repository
{
    using System.Data;
    using Fanex.Affiliate.DataService.Importer.DTO;
    using Fanex.Affiliate.DataService.Importer.Enum;
    using Fanex.Affiliate.DataService.Importer.Interface;
    using Fanex.Affiliate.DataService.Importer.Utilities;
    using Fanex.Data;

    public class CasinoDataImporterRepository : ICasinoDataImporterRepository
    {
        public ResultInfo ImportCasinoData(SystemType systemType, CasinoDataImporterMetaModel metaModel)
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

        private ResultInfo ImportDataFor12Bet(CasinoDataImporterMetaModel metaModel)
        {
            const string spName = "Aff_CasinoImportData";

            using (var objectDb = new ObjectDb(spName))
            {
                AssignCommonParam(objectDb, metaModel);
                objectDb.AddInParameter("importdate", DbType.DateTime, metaModel.DateImport);
                objectDb.AddInParameter("isreplacedata", DbType.Boolean, true);

                RepositoryHelper.AddResultParam(objectDb);

                objectDb.ExecuteNonQuery();

                return RepositoryHelper.GetResultInfo(objectDb);
            }
        }

        private ResultInfo ImportDataForIncomeAccess(CasinoDataImporterMetaModel metaModel)
        {
            const string spName = "Task_Aff_MemberDailyCasino_IA";

            using (var objectDb = new ObjectDb(spName))
            {
                AssignCommonParam(objectDb, metaModel);
                objectDb.AddInParameter("winlostdate", DbType.Date, metaModel.DateImport);

                RepositoryHelper.AddResultParam(objectDb);

                objectDb.ExecuteNonQuery();

                return RepositoryHelper.GetResultInfo(objectDb);
            }
        }

        private void AssignCommonParam(IObjectDb objectDb, CasinoDataImporterMetaModel metaModel)
        {
            objectDb.AddInParameter("adminid", DbType.Int32, metaModel.AdminId);
            objectDb.AddInParameter("xmldata", DbType.String, metaModel.XmlData);
        }
    }
}