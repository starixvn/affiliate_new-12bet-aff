namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    public interface IAppSettings : Common.Ultilities.IAppSettings
    {
        int CheckingTime { get; }

        bool ForTest { get; }

        int ForDate { get; }

        string ToMail { get; }

        string CcMail { get; }

        string BccMail { get; }

        string SubjectMailError { get; }
    }
}