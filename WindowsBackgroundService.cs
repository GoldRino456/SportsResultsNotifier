
namespace App.WindowsService;

public sealed class WindowsBackgroundService(SportsResultsNotificationService notifyService, ILogger<WindowsBackgroundService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await notifyService.SendNotificationEmailAsync();
                logger.LogWarning("Email Send Success Status: {result}", result);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            Environment.Exit(0);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Message}", ex.Message);
            Environment.Exit(1);
        }
    }
}
