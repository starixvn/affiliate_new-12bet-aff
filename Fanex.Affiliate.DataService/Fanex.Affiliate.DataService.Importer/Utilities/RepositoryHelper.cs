namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    using System;
    using System.Data;
    using Data;
    using Fanex.Affiliate.DataService.Importer.DTO;

    public static class RepositoryHelper
    {
        public static void AddResultParam(IObjectDb objectDb)
        {
            objectDb.AddOutParameter("ErrCode", DbType.Int32, 4);
            objectDb.AddOutParameter("ErrMsg", DbType.String, 4000);
        }

        public static ResultInfo GetResultInfo(IObjectDb objectDb)
        {
            var errorCode = objectDb.GetParameterValue("ErrCode");
            var errorMessage = objectDb.GetParameterValue("ErrMsg");

            return new ResultInfo
            {
                Code = Convert.ToInt32(errorCode),
                Message = Convert.ToString(errorMessage)
            };
        }
    }
}