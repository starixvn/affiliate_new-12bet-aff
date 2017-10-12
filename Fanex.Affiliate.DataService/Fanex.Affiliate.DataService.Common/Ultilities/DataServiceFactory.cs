namespace Fanex.Affiliate.DataService.Common.Ultilities
{
    public class DataServiceFactory : IDataServiceFactory
    {
        public IDataServiceLogger GetLogger()
        {
            return new DataServiceLogger();
        }

        public IEmailSender GetEmailSender()
        {
            return new EmailSender();
        }
    }
}