namespace Fanex.Affiliate.DataService.Importer.Repository
{
    using System;
    using System.Data;
    using Fanex.Affiliate.DataService.Importer.DTO;
    using Fanex.Data;

    public class BaseRepository
    {
        protected BaseRepository()
        {
        }

        protected static void AddResultParam(IObjectDb objectDb)
        {
            objectDb.AddOutParameter("ErrCode", DbType.Int32, 4);
            objectDb.AddOutParameter("ErrMsg", DbType.String, 4000);
        }

        protected static ResultInfo GetResultInfo(IObjectDb objectDb)
        {
            var errorCode = objectDb.GetParameterValue("ErrCode");
            var errorMessage = objectDb.GetParameterValue("ErrMsg");

            return new ResultInfo
            {
                Code = Convert.ToInt32(errorCode),
                Message = Convert.ToString(errorMessage)
            };
        }

        protected internal virtual IObjectDb GetObjectDb(string spname)
        {
            return new ObjectDb(spname);
        }
    }
}