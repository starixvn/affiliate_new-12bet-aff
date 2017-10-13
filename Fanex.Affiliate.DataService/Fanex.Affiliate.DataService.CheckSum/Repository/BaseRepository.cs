namespace Fanex.Affiliate.DataService.CheckSum.Repository
{
    using Fanex.Data;

    public class BaseRepository
    {
        protected internal virtual IObjectDb GetObjectDb(string spname)
        {
            return new ObjectDb(spname);
        }
    }
}