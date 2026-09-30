using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace SmileCraft.API.Services;

/// <summary>
/// Configuration settings for SMTP email delivery.
/// </summary>
public class SmtpSettings
{
    public string Server { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public string ToEmail { get; set; } = string.Empty;
    public string ToName { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } = true;
}

/// <summary>
/// Interface for sending emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="to">Recipient email address.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="body">Email body (HTML).</param>
    /// <returns>True if the email was sent successfully; otherwise, false.</returns>
    Task<bool> SendEmailAsync(string to, string subject, string body);
}

/// <summary>
/// Service for sending emails via SMTP.
/// </summary>
public class EmailService : IEmailService
{
    private readonly IOptionsMonitor<SmtpSettings> _settingsMonitor;
    private readonly ILogger<EmailService> _logger;
    private readonly string _templatePath;

    public EmailService(IOptionsMonitor<SmtpSettings> settingsMonitor, ILogger<EmailService> logger)
    {
        _settingsMonitor = settingsMonitor;
        _logger = logger;
        _templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmailTemplates", "ContactFormNotification.html");
    }

    private SmtpSettings _settings => _settingsMonitor.CurrentValue;

    /// <inheritdoc />
    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            using var client = new SmtpClient(_settings.Server, _settings.Port)
            {
                EnableSsl = _settings.EnableSsl,
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                DeliveryFormat = SmtpDeliveryFormat.International
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail, _settings.FromName),
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            };

            mailMessage.To.Add(new MailAddress(to, _settings.ToName));

            await client.SendMailAsync(mailMessage);

            _logger.LogInformation("Email sent successfully to {To} with subject {Subject}", to, subject);
            return true;
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, "SMTP error sending email to {To}: {ErrorMessage}", to, ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error sending email to {To}: {ErrorMessage}", to, ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Builds the HTML body for a contact form notification email.
    /// </summary>
    public string BuildContactFormEmail(string name, string email, string? phone, string message, DateTime submittedAt)
    {
        var template = LoadTemplate();
        var phoneDisplay = string.IsNullOrWhiteSpace(phone) ? "Not provided" : WebUtility.HtmlEncode(phone);

        return template
            .Replace("{{Name}}", WebUtility.HtmlEncode(name))
            .Replace("{{Email}}", WebUtility.HtmlEncode(email))
            .Replace("{{Phone}}", phoneDisplay)
            .Replace("{{Message}}", WebUtility.HtmlEncode(message).Replace("\n", "<br>"))
            .Replace("{{SubmittedAt}}", submittedAt.ToString("yyyy-MM-dd HH:mm:ss UTC"));
    }

    /// <summary>
    /// Loads the HTML email template from disk, or returns a default inline template.
    /// </summary>
    private string LoadTemplate()
    {
        try
        {
            if (File.Exists(_templatePath))
            {
                return File.ReadAllText(_templatePath);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to read email template from {Path}. Using default template.", _templatePath);
        }

        return GetDefaultTemplate();
    }

    /// <summary>
    /// Returns the default inline HTML email template.
    /// </summary>
    private static string GetDefaultTemplate()
    {
        return @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>New Contact Form Submission</title>
</head>
<body style=""margin: 0; padding: 0; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f4f4f4;"">
    <table role=""presentation"" style=""width: 100%; border-collapse: collapse; background-color: #f4f4f4;"">
        <tr>
            <td align=""center"" style=""padding: 40px 20px;"">
                <table role=""presentation"" style=""width: 100%; max-width: 600px; border-collapse: collapse; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1);"">
                    <!-- Header -->
                    <tr>
                        <td style=""background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); padding: 30px 40px; text-align: center;"">
                            <h1 style=""color: #ffffff; margin: 0; font-size: 24px; font-weight: 600;"">New Contact Form Submission</h1>
                            <p style=""color: rgba(255,255,255,0.9); margin: 8px 0 0 0; font-size: 14px;"">SmileCraft Dental Clinic</p>
                        </td>
                    </tr>
                    <!-- Body -->
                    <tr>
                        <td style=""padding: 40px;"">
                            <p style=""color: #333333; font-size: 16px; line-height: 1.6; margin: 0 0 24px 0;"">
                                A new message has been submitted through the contact form on the website.
                            </p>
                            <table role=""presentation"" style=""width: 100%; border-collapse: collapse; margin-bottom: 24px;"">
                                <tr>
                                    <td style=""padding: 12px 16px; background-color: #f8f9fa; border-left: 4px solid #667eea; width: 140px;"">
                                        <strong style=""color: #555555; font-size: 14px;"">Name</strong>
                                    </td>
                                    <td style=""padding: 12px 16px; background-color: #f8f9fa; border-left: 1px solid #e9ecef;"">
                                        <span style=""color: #333333; font-size: 14px;"">{{Name}}</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 12px 16px; background-color: #ffffff; border-left: 4px solid #667eea; width: 140px;"">
                                        <strong style=""color: #555555; font-size: 14px;"">Email</strong>
                                    </td>
                                    <td style=""padding: 12px 16px; background-color: #ffffff; border-left: 1px solid #e9ecef;"">
                                        <a href=""mailto:{{Email}}"" style=""color: #667eea; font-size: 14px; text-decoration: none;"">{{Email}}</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 12px 16px; background-color: #f8f9fa; border-left: 4px solid #667eea; width: 140px;"">
                                        <strong style=""color: #555555; font-size: 14px;"">Phone</strong>
                                    </td>
                                    <td style=""padding: 12px 16px; background-color: #f8f9fa; border-left: 1px solid #e9ecef;"">
                                        <span style=""color: #333333; font-size: 14px;"">{{Phone}}</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""padding: 12px 16px; background-color: #ffffff; border-left: 4px solid #667eea; width: 140px;"">
                                        <strong style=""color: #555555; font-size: 14px;"">Submitted At</strong>
                                    </td>
                                    <td style=""padding: 12px 16px; background-color: #ffffff; border-left: 1px solid #e9ecef;"">
                                        <span style=""color: #333333; font-size: 14px;"">{{SubmittedAt}}</span>
                                    </td>
                                </tr>
                            </table>
                            <table role=""presentation"" style=""width: 100%; border-collapse: collapse;"">
                                <tr>
                                    <td style=""padding: 16px; background-color: #f8f9fa; border-left: 4px solid #667eea;"">
                                        <strong style=""color: #555555; font-size: 14px; display: block; margin-bottom: 8px;"">Message</strong>
                                        <p style=""color: #333333; font-size: 14px; line-height: 1.6; margin: 0;"">{{Message}}</p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-- Footer -->
                    <tr>
                        <td style=""background-color: #f8f9fa; padding: 20px 40px; text-align: center; border-top: 1px solid #e9ecef;"">
                            <p style=""color: #999999; font-size: 12px; margin: 0;"">
                                This is an automated notification from SmileCraft Dental Clinic.<br>
                                Please do not reply to this email.
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
    }
}
