using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
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

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var client = new SendGridClient(_emailSettings.ApiKey);
                var from = new EmailAddress(_emailSettings.FromEmail, "Aspnetcore Mentee");
                var to = new EmailAddress(email);
                var plainTextContent = Regex.Replace(message, "<[^>]*>", "");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, message);
                await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Email cannot be sent");
                throw ex;
            }
        }
    }
}
