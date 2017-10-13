using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.DataService.CheckSum.DTO;
using Fanex.Affiliate.DataService.CheckSum.ViewModel;
using Fanex.Data;

namespace Fanex.Affiliate.DataService.CheckSum.Repository
{
    public class DailyMemberRepository : BaseRepository, IDailyMemberRepository
    {
        private static void AssignParam(IObjectDb objectDb, DateTime date)
        {
            objectDb.AddInParameter("date", DbType.Date, date.Date);
            objectDb.AddOutParameter("ismatch", DbType.Boolean, 4);
        }

        public DailyMemberViewModel CheckSumDailyMemberFor12Bet(DateTime date)
        {
            const string SpName = "Aff_CheckSum";
            IEnumerable<DailyMember> dailyMemberList;

            using (var objectDb = this.GetObjectDb(SpName))
            {
                AssignParam(objectDb, date);

                return new DailyMemberViewModel
                {
                    DateString = date.ToLongDateString(),
                    Data = objectDb.Query<DailyMember>(),
                    isMatch = Convert.ToBoolean(objectDb.GetParameterValue("ismatch"))
                };
            }
        }

        public DailyMemberIncomeAccessViewModel CheckSumDailyMemberForIncomeAccess(DateTime date)
        {
            const string SpName = "IA_CheckSum";

            using (var objectDb = this.GetObjectDb(SpName))
            {
                AssignParam(objectDb, date);

                return new DailyMemberIncomeAccessViewModel
                {
                    Data = objectDb.Query<DailyMemberIncomeAccess>(),
                    isMatch = Convert.ToBoolean(objectDb.GetParameterValue("ismatch")),
                    DateString = date.ToString()
                };
            }
        }
    }
}