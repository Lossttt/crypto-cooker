using System.Net;
using System.Net.Mail;
using crypto_app.Config.Options;
using crypto_app.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace crypto_app.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailOptions> emailOptions, ILogger<EmailService> logger)
        {
            _emailOptions = emailOptions.Value;
            _logger = logger;
        }

        public async Task SendPasswordResetEmailAsync(string email, string code, string firstName)
        {
            try
            {
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Services/Templates/password-reset-email-template.html");
                var emailBody = await File.ReadAllTextAsync(templatePath);
                emailBody = emailBody.Replace("{{CODE}}", code);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailOptions.SmtpUser, "Crypto Cooker"),
                    Subject = "Password Reset Request",
                    Body = emailBody,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                using var smtpClient = new SmtpClient(_emailOptions.SmtpServer, _emailOptions.SmtpPort)
                {
                    Credentials = new NetworkCredential(_emailOptions.SmtpUser, _emailOptions.SmtpPass),
                    EnableSsl = true
                };

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Password reset email sent successfully to {Email}", email);
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex, "Email template file not found.");
                throw;
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex, "Error sending email.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while sending password reset email.");
                throw;
            }
        }

        public async Task SendAccountVerificationEmailAsync(string email, string verificationUrl, string firstName)
        {
            try
            {
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Services/Templates/account-verification-email-template.html");
                var emailBody = await File.ReadAllTextAsync(templatePath);
                emailBody = emailBody.Replace("{{VERIFICATION_URL}}", verificationUrl);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailOptions.SmtpUser, "Crypto Cooker"),
                    Subject = "Account Verification",
                    Body = emailBody,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                using var smtpClient = new SmtpClient(_emailOptions.SmtpServer, _emailOptions.SmtpPort)
                {
                    Credentials = new NetworkCredential(_emailOptions.SmtpUser, _emailOptions.SmtpPass),
                    EnableSsl = true
                };

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Account verification email sent successfully to {Email}", email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while sending account verification email.");
                throw;
            }
        }
    }
}