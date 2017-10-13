namespace Fanex.Affiliate.DataService.CheckSum.Repository
{
    using System;
    using ViewModel;

    public interface IDailyMemberRepository
    {
        DailyMemberViewModel CheckSumDailyMemberFor12Bet(DateTime date);

        DailyMemberIncomeAccessViewModel CheckSumDailyMemberForIncomeAccess(DateTime date);
    }
}