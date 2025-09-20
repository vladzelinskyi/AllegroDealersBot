using AllegroDealersBot.Interfaces;
using AllegroDealersBot.Models;
using Microsoft.Extensions.Options;

namespace AllegroDealersBot.Services;

internal class CatalogFetcher : ICatalogFetcher
{
    private string _url;
    private readonly AllegroSettings _settings;
    private readonly ILogger<CatalogFetcher> _logger;
    private readonly CatalogSerializer _serializer;

    public CatalogFetcher(IOptions<AllegroSettings> settings, ILogger<CatalogFetcher> logger, CatalogSerializer serializer)
    {
        _settings = settings.Value;
        _logger = logger;
        _url = _settings.CatalogUrl;
        _serializer = serializer;
    }

    public YmlCatalog FetchCatalog()
    {
        YmlCatalog catalog;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_url);
            var response = client.GetAsync(_url).Result;

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                catalog = _serializer.DeserializeXml(content);
            }
            else
            {
                catalog = new YmlCatalog();
            }
        }

        return catalog;
    }
}