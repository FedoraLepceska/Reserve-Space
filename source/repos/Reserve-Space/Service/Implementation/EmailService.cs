using Domain;
using Domain.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            this._settings = settings;
        }
        public async Task SendEmailAsync(List<EmailMessage> emailMessages)
        {
            List<MimeMessage> messages = new List<MimeMessage>();

            foreach(var item in emailMessages)
            {
                var message = new MimeMessage
                {
                    Sender = new MailboxAddress(_settings.SenderName, _settings.SmtpUsername),
                    Subject = item.Subject
                };

                message.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.SmtpUsername));
                message.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = item.Content };
                message.To.Add(new MailboxAddress(item.MailTo, ""));
                messages.Add(message);
            }

            try
            {
                using(var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOption = _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;

                    await smtp.ConnectAsync(_settings.SmtpServer,
                                            _settings.SmtpServerPort,
                                            socketOption);

                    if (string.IsNullOrEmpty(_settings.SmtpUsername))
                    {
                        await smtp.AuthenticateAsync(_settings.SmtpUsername, _settings.SmtpPassword);
                    }

                    foreach (var item in messages)
                    {
                        await smtp.SendAsync(item);
                    }

                    await smtp.DisconnectAsync(true);
                }
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
    }
}
