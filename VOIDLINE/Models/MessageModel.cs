namespace VOIDLINE.Models;

public class MessageModel
{
    private string _subject { get; set; }

    private string _message { get; set; }

    private string? _code { get; set; } = null;

    public MessageModel(string subject, string message, string? code)
    {
        _subject = subject;
        _message = message;
        _code = code;
    }

    public string GetSubject()
    {
        return _subject;
    }

    public string GetMessage()
    {
        return _message;
    }

    public string GetCode()
    {
        return _code;
    }
}
