namespace Fanex.Affiliate.DataService.CheckSum.Repository
{
    using System;
    using System.Collections.Generic;
    using DTO;
    using ViewModel;

    public interface IDailyMemberRepository
    {
        DailyMemberViewModel CheckSumDailyMemberFor12Bet(DateTime date);

        DailyMemberIncomeAccessViewModel CheckSumDailyMemberForIncomeAccess(DateTime date);
    }
}