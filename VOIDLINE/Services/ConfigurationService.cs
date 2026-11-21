namespace VOIDLINE.Services;

public static class ConfigurationService
{
    public static IConfiguration? BuildConfig()
    {
        try
        {
            return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }
        catch (Exception)
        {
            return null;
        }
    }
}
