namespace Fanex.AffiliatePortal.Job.DataAccess
{
    using System;
    using System.Data;
    using Fanex.AffiliatePortal.Job.DTO;
    using Fanex.Data;

    public static class DataAccessHelper
    {
        public static void AddResultParam(IObjectDb objectDb)
        {
            objectDb.AddOutParameter("ErrCode", DbType.Int32, 4);
            objectDb.AddOutParameter("ErrMsg", DbType.String, 4000);
        }

        public static JobResult GetResultInfo(IObjectDb objectDb)
        {
            var errorCode = objectDb.GetParameterValue("ErrCode");
            var errorMessage = objectDb.GetParameterValue("ErrMsg");

            return new JobResult
            {
                Code = Convert.ToInt32(errorCode),
                Message = Convert.ToString(errorMessage)
            };
        }
    }
}
