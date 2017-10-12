namespace Fanex.Affiliate.DataService.Common.Ultilities
{
    using Fanex.Affiliate.DataService.Common.DTO;

    public interface IEmailSender
    {
        void Send(Email mailSending);
    }
}