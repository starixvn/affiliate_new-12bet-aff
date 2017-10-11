using System;
using System.Data;
using Fanex.Data;

namespace Fanex.AffiliatePortal.Job.CheckSum.DataAccess
{
    public class MemberDailyDataAccess
    {
        public bool CheckSumMemberDaily(DateTime date)
        {
            const string spName = "Aff_CheckSum";

            return ExecuteCheckSumMemberDaily(spName, date);
        }

        public bool CheckSumMemberDailyForImcomeAccess(DateTime date)
        {
            const string spName = "IA_CheckSum";

            return ExecuteCheckSumMemberDaily(spName, date);
        }

        private bool ExecuteCheckSumMemberDaily(string spName, DateTime date)
        {
            using (var objectDb = new ObjectDb(spName))
            {
                objectDb.AddInParameter("date", DbType.Date, date.Date);
                objectDb.AddOutParameter("ismatch", DbType.Boolean, 4);

                objectDb.ExecuteNonQuery();

                return Convert.ToBoolean(objectDb.GetParameterValue("ismatch"));
            }
        }
    }
}