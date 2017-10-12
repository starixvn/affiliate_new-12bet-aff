namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public interface IAppSettings
    {
        int CheckingTime1 { get; }

        int CheckingTime2 { get; }

        string DailyDataUrl { get; }

        string FileExtension { get; }

        int ForSystem { get; }

        bool ForTest { get; }

        string PathSaveFile { get; }

        int AdminId { get; }

        string ToMail { get; }

        string CcMail { get; }

        string BccMail { get; }

        string SubjectMailError { get; }

        string GetValue(string key);
    }
}