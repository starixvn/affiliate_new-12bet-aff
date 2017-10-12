namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    public interface IAppSettings
    {
        int CheckingTime { get; }

        bool ForTest { get; }

        int ForDate { get; }

        string ToMail { get; }

        string CcMail { get; }

        string BccMail { get; }

        string SubjectMailError { get; }

        string GetValue(string key);
    }
}