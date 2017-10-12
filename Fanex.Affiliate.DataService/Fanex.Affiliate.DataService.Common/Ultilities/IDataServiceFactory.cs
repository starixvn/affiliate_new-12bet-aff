namespace Fanex.Affiliate.DataService.Common.Ultilities
{
    public interface IDataServiceFactory
    {
        IEmailSender GetEmailSender();

        IDataServiceLogger GetLogger();
    }
}
