namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using Fanex.Affiliate.DataService.CheckSum.Enum;

    public interface IEmailContentBuilder
    {
        string Build(SystemType systemType, object dailyMemberList, int dayToAdd);
    }
}