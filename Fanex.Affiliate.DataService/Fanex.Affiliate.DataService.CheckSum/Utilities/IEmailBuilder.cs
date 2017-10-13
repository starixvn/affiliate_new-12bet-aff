namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System.Collections.Generic;
    using DTO;
    using ViewModel;

    public interface IEmailBuilder
    {
        string GenerateDailyMemberContent(DailyMemberViewModel data);

        string GenerateDailyMemberIncomeAccessContent(DailyMemberIncomeAccessViewModel data);
    }
}