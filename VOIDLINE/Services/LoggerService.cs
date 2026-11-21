namespace VOIDLINE.Services;

public class LoggerService
{
    private readonly ILogger<LoggerService> _logger;

    public LoggerService(ILogger<LoggerService> logger)
    {
        _logger = logger;
    }

    public void LogError(string message)
    {
        _logger.LogError("{Message}", message);
    }

    public void LogWarning(string message)
    {
        _logger.LogWarning("{Message}", message);
    }
}
