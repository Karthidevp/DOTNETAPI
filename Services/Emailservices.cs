using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Services
{
    public class Emailservices:IEmailservices
    {
        private readonly IConfiguration _config;

        public Emailservices(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(EmailRequest request)
        {
            var smtpClient = new SmtpClient(_config["MailSettings:MailServer"])
            {
                Port = int.Parse(_config["MailSettings:Port"]!),
                Credentials = new NetworkCredential(
                    _config["MailSettings:EmailId"],
                    _config["MailSettings:Password"]
                ),
                EnableSsl = true,
            };

            // Load HTML template
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "EmailTemplate.html");
            string body = await File.ReadAllTextAsync(templatePath);

            // Replace placeholders with actual values
            body = body.Replace("{{Name}}", request.Name)
                       .Replace("{{Email}}", request.Email)
                       .Replace("{{Message}}", request.Message);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["MailSettings:EmailId"], "Your App"),
                Subject = $"📩 Message from {request.Name}",
                Body = body,
                IsBodyHtml = true  // ✅ Important for HTML content
            };

            // Send to the recipient provided in the model
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                mailMessage.To.Add(request.Email.Trim());
            }
            else
            {
                throw new ArgumentException("Recipient email is required.");
            }

            await smtpClient.SendMailAsync(mailMessage);
        }

        //public async Task SendEmailAsync(EmailRequest request)
        //{
        //    var smtpClient = new SmtpClient(_config["MailSettings:MailServer"])
        //    {
        //        Port = int.Parse(_config["MailSettings:Port"]!),
        //        Credentials = new NetworkCredential(
        //            _config["MailSettings:EmailId"],
        //            _config["MailSettings:Password"]
        //        ),
        //        EnableSsl = true,
        //    };

        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress(_config["MailSettings:EmailId"]),
        //        Subject = $"Message from {request.Name}",
        //        Body = $"From: {request.Name} ({request.Email})\n\n{request.Message}",
        //        IsBodyHtml = false,
        //    };

        //    // ✅ Send directly to the email provided in the request
        //    if (!string.IsNullOrWhiteSpace(request.Email))
        //    {
        //        mailMessage.To.Add(request.Email.Trim());
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Recipient email is required.");
        //    }

        //    await smtpClient.SendMailAsync(mailMessage);
        //}

    }
}
