namespace Fanex.Affiliate.DataService.CheckSum.Repository
{
    using System;
    using System.Data;

    public interface IDailyMemberRepository
    {
        DataTable CheckSumDailyMemberFor12Bet(DateTime date, out bool isMatch);

        DataTable CheckSumDailyMemberForIncomeAccess(DateTime date, out bool isMatch);
    }
}