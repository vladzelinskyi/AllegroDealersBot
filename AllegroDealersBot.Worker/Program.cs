using AllegroDealersBot.Models;
using AllegroDealersBot.Worker;
using AllegroDealersBot.Worker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<AllegroSettings>(hostContext.Configuration.GetSection("AppSettings"));
        
        services.AddHostedService<Worker>();

        services.AddSingleton<CatalogFetcher>();

        services.AddSingleton<CatalogComparator>();
        
        services.AddSingleton<CatalogSerializer>();
    })
    .Build();

host.Run();