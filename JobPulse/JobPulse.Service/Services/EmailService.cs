using JobPulse.Data.Models;
using JobPulse.Service.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace JobPulse.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IOptions<EmailSetting> options,
            ILogger<EmailService> logger)
        {
            _emailSettings = options.Value;
            _logger = logger;
        }

        public async Task SendEmailJobAsync(IEnumerable<JobPosting> jobs)
        {
            try
            {
                List<JobPosting> jobList = jobs.ToList();

                if (jobList.Count == 0)
                {
                    return;
                }

                StringBuilder body = new StringBuilder();

                body.AppendLine(
                    $"JobPulse found {jobList.Count} new job(s).");

                body.AppendLine();

                foreach (JobPosting job in jobList)
                {
                    body.AppendLine($"Title: {job.Title}");
                    body.AppendLine($"Company: {job.Company}");
                    body.AppendLine($"Location: {job.Location}");
                    body.AppendLine($"Posted: {job.PostedDate}");
                    body.AppendLine($"Link: {job.JobUrl}");

                    body.AppendLine();
                    body.AppendLine("------------------------------");
                    body.AppendLine();
                }

                using MailMessage message = new MailMessage();

                message.From =
                    new MailAddress(_emailSettings.FromEmail);

                message.To.Add(_emailSettings.ToEmail);

                message.Subject =
                    $"JobPulse - {jobList.Count} new job(s)";

                message.Body = body.ToString();

                using SmtpClient smtpClient = new SmtpClient(
                    _emailSettings.SmtpHost,
                    _emailSettings.SmtpPort);

                smtpClient.EnableSsl = true;

                smtpClient.Credentials =
                    new NetworkCredential(
                        _emailSettings.FromEmail,
                        _emailSettings.Password);

                await smtpClient.SendMailAsync(message);

                _logger.LogInformation(
                    "Successfully sent email notification for {JobCount} jobs.",
                    jobList.Count);
            }
            catch (Exception)
            {

            }
        }
    }
}
