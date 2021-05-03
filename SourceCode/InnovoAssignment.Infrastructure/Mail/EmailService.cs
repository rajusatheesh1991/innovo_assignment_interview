using Microsoft.Extensions.Options;
using InnovoAssignment.Application.Contracts.Infrastructure;
using InnovoAssignment.Application.Models;
using System.Threading.Tasks;
using MimeKit;
using System.Net.Mail;
using System;
using InnovoAssignment.Utilities;

namespace InnovoAssignment.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task<bool> SendEmail(EmailDto email)
        {


            string to = email.To;
            string from = StringConstants.EMAIL_FROM_ADDRESS_USERNAME;
            MailMessage message = new MailMessage(from, to);
            message.Subject = email.Subject;
            message.Body = email.Body;
            SmtpClient client = new SmtpClient(StringConstants.SMTP_SERVER);
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials= new System.Net.NetworkCredential(StringConstants.EMAIL_FROM_ADDRESS_USERNAME,
                StringConstants.EMAIL_PASSWORD);

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }


            return Task.FromResult(true);

        }

      

    }
}
