using System;
using System.Data;
using Fanex.Data;

namespace Fanex.Affiliate.DataService.CheckSum.Repository
{
    public class DailyMemberRepository : IDailyMemberRepository
    {
        public DataTable CheckSumDailyMemberFor12Bet(DateTime date, out bool isMatch)
        {
            const string SpName = "Aff_CheckSum";

            return GetCheckSumDailyMemberData(SpName, date, out isMatch);
        }

        public DataTable CheckSumDailyMemberForIncomeAccess(DateTime date, out bool isMatch)
        {
            const string SpName = "IA_CheckSum";

            return GetCheckSumDailyMemberData(SpName, date, out isMatch);
        }

        private DataTable GetCheckSumDailyMemberData(string uspName, DateTime date, out bool isMatch)
        {
            using (var objectDb = new ObjectDb(uspName))
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