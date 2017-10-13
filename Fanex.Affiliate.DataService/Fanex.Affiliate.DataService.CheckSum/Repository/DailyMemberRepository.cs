using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.DataService.CheckSum.DTO;
using Fanex.Data;

namespace Fanex.Affiliate.DataService.CheckSum.Repository
{
    public class DailyMemberRepository : BaseRepository, IDailyMemberRepository
    {
        public IEnumerable<DailyMember> CheckSumDailyMemberFor12Bet(DateTime date, out bool isMatch)
        {
            const string SpName = "Aff_CheckSum";
            IEnumerable<DailyMember> dailyMemberList;

            using (var objectDb = this.GetObjectDb(SpName))
            {
                AssignParam(objectDb, date);

                dailyMemberList = objectDb.Query<DailyMember>();

                isMatch = Convert.ToBoolean(objectDb.GetParameterValue("ismatch"));
            }

            return dailyMemberList;
        }

        public IEnumerable<DailyMemberIncomeAccess> CheckSumDailyMemberForIncomeAccess(DateTime date, out bool isMatch)
        {
            const string SpName = "IA_CheckSum";

            IEnumerable<DailyMemberIncomeAccess> dailyMemberIncomeAccessList;

            using (var objectDb = this.GetObjectDb(SpName))
            {
                AssignParam(objectDb, date);

                dailyMemberIncomeAccessList = objectDb.Query<DailyMemberIncomeAccess>();

                isMatch = Convert.ToBoolean(objectDb.GetParameterValue("ismatch"));
            }

            return dailyMemberIncomeAccessList;
        }

        private static void AssignParam(IObjectDb objectDb, DateTime date)
        {
            objectDb.AddInParameter("date", DbType.Date, date.Date);
            objectDb.AddOutParameter("ismatch", DbType.Boolean, 4);
        }
    }
}