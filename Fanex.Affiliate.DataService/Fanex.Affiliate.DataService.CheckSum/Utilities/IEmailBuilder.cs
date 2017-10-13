namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System.Collections.Generic;
    using DTO;
    using Fanex.Affiliate.DataService.CheckSum.Enum;

    public interface IEmailBuilder
    {
        string Build(SystemType systemType, object dailyMemberList, int dayToAdd);

        string GenerateDailyMemberContent(IList<DailyMember> data, int dayToAdd);

        string GenerateIncomeAccessContent(IList<DailyMemberIncomeAccess> data, int dayToAdd);
    }
}