using System.Data;
using Fanex.AffiliatePortal.Job.DataAccess;
using Fanex.AffiliatePortal.Job.DataImporting.Enum;
using Fanex.AffiliatePortal.Job.DataImporting.MetaModel;
using Fanex.AffiliatePortal.Job.DTO;
using Fanex.Data;

namespace Fanex.AffiliatePortal.Job.DataImporting.DataAccess
{
    public class CasinoDataImportingDataAccess
    {
        public JobResult ImportCasinoData(SystemType systemType, CasinoDataImportingMetaModel metaModel)
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
                    return new JobResult();
            }
        }

        private JobResult ImportDataFor12Bet(CasinoDataImportingMetaModel metaModel)
        {
            const string spName = "Aff_CasinoImportData";

            using (var objectDb = new ObjectDb(spName))
            {
                AssignCommonParam(objectDb, metaModel);
                objectDb.AddInParameter("importdate", DbType.DateTime, metaModel.DateImport);
                objectDb.AddInParameter("isreplacedata", DbType.Boolean, true);

                DataAccessHelper.AddResultParam(objectDb);

                objectDb.ExecuteNonQuery();

                return DataAccessHelper.GetResultInfo(objectDb);
            }
        }

        private JobResult ImportDataForIncomeAccess(CasinoDataImportingMetaModel metaModel)
        {
            const string spName = "Task_Aff_MemberDailyCasino_IA";

            using (var objectDb = new ObjectDb(spName))
            {
                AssignCommonParam(objectDb, metaModel);
                objectDb.AddInParameter("winlostdate", DbType.Date, metaModel.DateImport);

                DataAccessHelper.AddResultParam(objectDb);

                objectDb.ExecuteNonQuery();

                return DataAccessHelper.GetResultInfo(objectDb);
            }
        }

        private void AssignCommonParam(IObjectDb objectDb, CasinoDataImportingMetaModel metaModel)
        {
            objectDb.AddInParameter("adminid", DbType.Int32, metaModel.AdminId);
            objectDb.AddInParameter("xmldata", DbType.String, metaModel.XmlData);
        }
    }
}
