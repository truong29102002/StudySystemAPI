using Microsoft.Extensions.Logging;
using MimeKit;
using StudySystem.Application.Service.Interfaces;
using StudySystem.Data.Models.Request;
using StudySystem.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StudySystem.Application.Service
{
    public class SendMailService : ISendMailService
    {
        private readonly ILogger<SendMailService> logger;
        public SendMailService(ILogger<SendMailService> _logger)
        {
            logger = _logger;
            logger.LogInformation("Create SendMailService");
            
        }
        public async Task<bool> SendMailAsync(string username, string verificationCode)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = new MailboxAddress(AppSetting.MailName, AppSetting.Mail);
                email.From.Add(new MailboxAddress(AppSetting.MailName, AppSetting.Mail));

                email.To.Add(new MailboxAddress(username, username));
                email.Subject = "Verify code";

                var builder = new BodyBuilder();
                builder.HtmlBody = Body(verificationCode);
                email.Body = builder.ToMessageBody();

                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    await smtp.ConnectAsync(AppSetting.MailHost, AppSetting.MailPort, MailKit.Security.SecureSocketOptions.StartTls).ConfigureAwait(false);
                    await smtp.AuthenticateAsync(AppSetting.Mail, AppSetting.MailPassword).ConfigureAwait(false);
                    await smtp.SendAsync(email).ConfigureAwait(false);
                    smtp.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        private string Body(string code)
        {
            return $"<h1>Xác minh tài khoản</h1><h3>Mã bảo mật tồn tại trong 5 phút</h3>Vui lòng sử dụng mã bảo mật sau cho tài khoản.<br/><br/>Mã bảo mật: {code}<br/><br/>Xin cám ơn.";
        }

        public bool VerificationCode(string code, string verifyCode, DateTime expireCode)
        {
            try
            {
                if (code.Equals(verifyCode) && expireCode > DateTime.UtcNow)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
            }
            return false;
        }
    }
}
