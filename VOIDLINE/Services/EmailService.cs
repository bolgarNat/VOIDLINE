using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using VOIDLINE.Models;

namespace VOIDLINE.Services;

public class EmailService
{
    private IConfiguration? _config; 
    
    private readonly LoggerService _loggerService;

    private MessageModel _messageModel;

    public EmailService(LoggerService loggerService)
    {
        _loggerService = loggerService;
    }

    public MessageModel GetMessageModel()
    {
        return _messageModel;
    }

    public async Task<bool> SendEmail(string email, string? subject, string? message)
    {
        _config ??= ConfigurationService.BuildConfig();

        if (subject == null || message == null)
        {
            _messageModel = await CodeVarificationService.ConfigVarificationMessage();
        }
        else
        {
            _messageModel = new MessageModel(subject, message, null);
        }

        using var emailMessage = new MimeMessage();

        try
        {
            var emailAddress = _config!.GetValue<string>("EmailSettings:EmailAddress");
            var emailPassword = _config!.GetValue<string>("EmailSettings:EmailPasswd");

            emailMessage.From.Add(new MailboxAddress("VOIDLINE", emailAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = _messageModel.GetSubject();
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = _messageModel.GetMessage()
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(emailAddress, emailPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }

            return true;
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex.ToString());
            return false;
        }
    }
}
