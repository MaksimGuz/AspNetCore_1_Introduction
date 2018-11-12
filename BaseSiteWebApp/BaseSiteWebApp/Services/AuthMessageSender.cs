using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public AuthMessageSender(IOptions<EmailSettings> emailSettings, ILogger<AuthMessageSender> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public EmailSettings _emailSettings { get; }

        private ILogger<AuthMessageSender> _logger;

        public Task SendEmailAsync(string email, string subject, string message)
        {

            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string email, string subject, string message)
        {
            try
            {                
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Aspnetcore Mentee"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };
                mail.To.Add(new MailAddress(email));

                using (SmtpClient smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port))
                {                    
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Email cannot be sent");
                throw ex;
            }
        }
    }
}
