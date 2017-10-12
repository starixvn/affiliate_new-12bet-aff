using Fanex.Affiliate.DataService.Common.DTO;
using Fanex.MailDeliver.MailClient.API;
using Fanex.MailDeliver.Model;

namespace Fanex.Affiliate.DataService.Common.Ultilities
{
    public class EmailSender : IEmailSender
    {
        private readonly IMailDeliver _mailDeliver;

        public EmailSender() : this(null)
        {
        }

        public EmailSender(IMailDeliver mailDeliver)
        {
            _mailDeliver = mailDeliver ?? new MailDeliver.MailClient.API.MailDeliver();
        }

        public void Send(Email mailSending)
        {
            MailRecord mailItem = new MailRecord
            {
                MailTo = mailSending.To,
                Subject = mailSending.Subject,
                Title = mailSending.Title,
                Body = mailSending.Body,
                MailBcc = mailSending.Bcc,
                MailCc = mailSending.Cc
            };

            _mailDeliver.SendMail(mailItem);
        }
    }
}
