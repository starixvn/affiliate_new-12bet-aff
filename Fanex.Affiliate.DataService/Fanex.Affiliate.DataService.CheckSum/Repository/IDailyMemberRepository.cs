namespace Fanex.Affiliate.DataService.CheckSum.Repository
{
    using System;
    using System.Collections.Generic;
    using DTO;

    public interface IDailyMemberRepository
    {
        IEnumerable<DailyMember> CheckSumDailyMemberFor12Bet(DateTime date, out bool isMatch);

        IEnumerable<DailyMemberIncomeAccess> CheckSumDailyMemberForIncomeAccess(DateTime date, out bool isMatch);
    }
}