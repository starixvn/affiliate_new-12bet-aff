namespace Fanex.Affiliate.DataService.Common.Ultilities
{
    using System;
    using Fanex.Logging;

    public class DataServiceLogger : IDataServiceLogger
    {
        public void LogError(string message, Exception exception)
        {
            LogService.Error(message, exception);
        }

        public void LogError(string message)
        {
            LogService.Error(message);
        }

        public void LogError(string message, string category)
        {
            LogService.Error(message, category);
        }

        public void LogInfo(string message)
        {
            LogService.Info(message);
        }
    }
}
