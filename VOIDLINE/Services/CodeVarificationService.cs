using VOIDLINE.Models;

namespace VOIDLINE.Services;

public static class CodeVarificationService
{
    private static string GenereteCode()
    {
        Random rand = new Random();
        string code = "";

        for (int i = 0; i < 4; i++)
        {
            code = code + (rand.Next(9).ToString());
        }

        return code;
    }

    public static async Task<MessageModel> ConfigVarificationMessage()
    {
        var subject = "Verification Code";
        var message = await File.ReadAllTextAsync("wwwroot/varification_code_message.html");

        var code = GenereteCode();
        message = message.Replace("{{CODE}}", code);

        MessageModel model = new MessageModel(subject, message, code);

        return model;
    }
}
