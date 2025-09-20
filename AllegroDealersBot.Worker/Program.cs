using AllegroDealersBot.Models;
using AllegroDealersBot.Services;
using AllegroDealersBot.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<AllegroSettings>(hostContext.Configuration.GetSection("AppSettings"));
        
        services.AddHostedService<Worker>();

        services.AddSingleton<CatalogFetcher>();
        
        services.AddSingleton<CatalogSerializer>();
    })
    .Build();

host.Run();