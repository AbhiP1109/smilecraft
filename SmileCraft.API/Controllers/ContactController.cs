using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmileCraft.API.Models;
using SmileCraft.API.Services;

namespace SmileCraft.API.Controllers;

/// <summary>
/// Controller for handling contact form submissions.
/// </summary>
[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    private readonly ILogger<ContactController> _logger;
    private readonly IEmailService _emailService;
    private readonly SmtpSettings _smtpSettings;

    public ContactController(
        ILogger<ContactController> logger,
        IEmailService emailService,
        IOptions<SmtpSettings> smtpSettings)
    {
        _logger = logger;
        _emailService = emailService;
        _smtpSettings = smtpSettings.Value;
    }

    /// <summary>
    /// Submits a contact form message.
    /// </summary>
    /// <param name="message">The contact message from the patient.</param>
    /// <returns>A success response if the message was received.</returns>
    /// <response code="200">Message received successfully.</response>
    /// <response code="400">If the message is invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SubmitContactForm([FromBody] ContactMessage message)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                Success = false,
                Message = "Validation failed.",
                Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
            });
        }

        // Log the contact message (no database — just logging)
        _logger.LogInformation(
            "New contact form submission from {Name} ({Email}) at {SubmittedAt}",
            message.Name,
            message.Email,
            message.SubmittedAt
        );

        // Send email notification to the clinic
        try
        {
            var subject = $"New Contact Form Submission - {message.Name}";
            var body = (_emailService as EmailService)?.BuildContactFormEmail(
                message.Name,
                message.Email,
                message.Phone,
                message.Message,
                message.SubmittedAt
            ) ?? string.Empty;

            var emailSent = await _emailService.SendEmailAsync(
                _smtpSettings.ToEmail,
                subject,
                body
            );

            if (emailSent)
            {
                _logger.LogInformation("Contact form email sent successfully to {ToEmail}", _smtpSettings.ToEmail);
            }
            else
            {
                _logger.LogWarning("Failed to send contact form email to {ToEmail}", _smtpSettings.ToEmail);
            }
        }
        catch (Exception ex)
        {
            // Don't fail the request if email fails — just log the error
            _logger.LogError(ex, "Error sending contact form email for {Name} ({Email})", message.Name, message.Email);
        }

        return Ok(new
        {
            Success = true,
            Message = "Thank you for contacting us! We will get back to you within 24 hours.",
            Data = new
            {
                Name = message.Name,
                Email = message.Email,
                SubmittedAt = message.SubmittedAt
            }
        });
    }
}
