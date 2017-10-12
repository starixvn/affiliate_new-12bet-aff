using System;
using System.Data;
using Fanex.Affiliate.DataService.CheckSum.Interface;
using Fanex.Data;

namespace Fanex.Affiliate.DataService.CheckSum.Repository
{
    public class DailyMemberRepository : IDailyMemberRepository
    {
        public DataTable CheckSumDailyMemberFor12Bet(DateTime date, out bool isMatch)
        {
            const string spName = "Aff_CheckSum";

            return GetCheckSumDailyMemberData(spName, date, out isMatch);
        }

        public DataTable CheckSumDailyMemberForIncomeAccess(DateTime date, out bool isMatch)
        {
            const string spName = "IA_CheckSum";

            return GetCheckSumDailyMemberData(spName, date, out isMatch);
        }

        private DataTable GetCheckSumDailyMemberData(string spName, DateTime date, out bool isMatch)
        {
            using (var objectDb = new ObjectDb(spName))
            {
                objectDb.AddInParameter("date", DbType.Date, date.Date);
                objectDb.AddOutParameter("ismatch", DbType.Boolean, 4);

                var dataset = objectDb.ExecuteDataSet();

                isMatch = Convert.ToBoolean(objectDb.GetParameterValue("ismatch"));

                return dataset.Tables[0];
            }
        }
    }
}