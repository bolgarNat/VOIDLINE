using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace VOIDLINE.Services;

public class EmailService
{
    private IConfiguration? _config; 
    
    private readonly LoggerService _loggerService;

    private string _code;

    public EmailService(LoggerService loggerService)
    {
        _loggerService = loggerService;
    }

    public string GetCode()
    {
        return _code;
    }

    public async Task<bool> SendEmail(string email, string? subject, string? message)
    {
        _config ??= ConfigurationService.BuildConfig();

        if (subject == null || message == null)
        {
            subject = "Verification Code";
            message = await File.ReadAllTextAsync("wwwroot/varification_code_message.html");

            _code = GenereteCode();
            message = message.Replace("{{CODE}}", _code);
        }

        using var emailMessage = new MimeMessage();

        try
        {
            var emailAddress = _config!.GetValue<string>("EmailSettings:EmailAddress");
            var emailPassword = _config!.GetValue<string>("EmailSettings:EmailPasswd");

            emailMessage.From.Add(new MailboxAddress("VOIDLINE", emailAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
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

    private string GenereteCode()
    {
        Random rand = new Random();
        string code = "";

        for (int i = 0; i<4; i++)
        {
            code = code + (rand.Next(9).ToString());
        }

        return code;
    }
}
