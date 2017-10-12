namespace Fanex.Affiliate.DataService.Common.Ultilities
{
    using System;

    public interface IDataServiceLogger
    {
        void LogInfo(string message);

        void LogError(string message);

        void LogError(string message, string category);

        void LogError(string message, Exception exception);
    }
}