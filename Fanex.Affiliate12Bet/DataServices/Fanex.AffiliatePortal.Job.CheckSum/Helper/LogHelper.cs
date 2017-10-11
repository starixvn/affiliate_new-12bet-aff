using System;
using System.Data.SqlClient;
using System.Text;
using Fanex.Logging;

namespace Fanex.AffiliatePortal.Job.CheckSum.Helper
{
    public static class LogHelper
    {
        public static void WriteErrorLog(string message, Exception exception)
        {
            var content = new StringBuilder().AppendLine(message).AppendLine(exception.Message);

            content.AppendLine("SOURCE: " + exception.Source)
                   .AppendLine("TARGETSITE: " + exception.TargetSite)
                   .AppendLine("STACKTRACE: " + exception.StackTrace);

            var sqlException = exception as SqlException;
            if (sqlException != null)
            {
                content.AppendLine("Procedure: " + sqlException.Procedure)
                    .AppendLine("Line Number: " + sqlException.LineNumber)
                    .AppendLine("Server: " + sqlException.Server);
            }

            LogService.Error(content.ToString(), exception);
        }
    }
}
