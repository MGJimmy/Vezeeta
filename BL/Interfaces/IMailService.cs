using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IMailService
    {

        Task SendEmailAsync(string toEmail, string subject, string content);
    }
    public class SendGridMailService : IMailService
    {
        
        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("kerolosSoliman980@gmail.com", "vezeeta");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content,content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
