using System;
using Fanex.AffiliatePortal.Job.CheckSum.Configuration;
using Fanex.AffiliatePortal.Job.CheckSum.DataAccess;
using Fanex.AffiliatePortal.Job.CheckSum.Helper;
using Fanex.Logging;

namespace Fanex.AffiliatePortal.Job.CheckSum
{
    public class MemberDailyCheckSum
    {
        public void Run()
        {
            try
            {
                //decide time to compare data(check data 2 times: first at 11:00-12:00, second at 23:00-24:00)
                if (DateTime.Now.Hour == AppSettings.CheckingTime || AppSettings.ForTest)
                {
                    //Call SPs check Data here
                    DateTime date = DateTime.Now.AddDays(AppSettings.ForDate);
                    var memberDailyDataAccess = new MemberDailyDataAccess();
                    var isMatch = memberDailyDataAccess.CheckSumMemberDaily(date);

                    //if failed: send email to IT team
                    if (!isMatch)
                    {
                        //SendEmail(AppSettings.SubjectMailError, buildBodyEmail(rs.ResultSet.Tables[0]));

                    }

                    LogService.Info("AffCheckSum run for date: " + DateTime.Now.AddDays(AppSettings.ForDate).ToShortDateString() + "; IsMatch: " + isMatch);

                    isMatch = memberDailyDataAccess.CheckSumMemberDailyForImcomeAccess(date);
                    if (!isMatch)
                    {
                        //SendEmail(AppSettings.SubjectMailError, IA_BuildBodyEmail(rs1.ResultSet.Tables[0]));

                    }

                    LogService.Info("Income Access check sum run for date: " + DateTime.Now.AddDays(AppSettings.ForDate).ToShortDateString() + "; IsMatch: " + isMatch);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog("AFFCheckSum", ex);
                //SendEmail(AppSettings.SubjectMailError, ex.Message);
            }
        }
    }
}
