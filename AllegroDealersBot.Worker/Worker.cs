using AllegroDealersBot.Services;

namespace AllegroDealersBot.Worker;

internal class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly CatalogFetcher _catalogFetcher;

    public Worker(ILogger<Worker> logger, CatalogFetcher  catalog)
    {
        _logger = logger;
        _catalogFetcher =  catalog;

        var cat = _catalogFetcher.FetchCatalog();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
