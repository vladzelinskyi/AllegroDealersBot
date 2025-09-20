using AllegroDealersBot.Interfaces;
using AllegroDealersBot.Models;

namespace AllegroDealersBot.Services;

internal class CatalogFetcher : ICatalogFetcher
{
    // private string url = ConfigurationManager.AppSettings["CatalogUrl"];
    private readonly ISerializer _serializer;

    public CatalogFetcher(ISerializer serializer)
    {
        _serializer = serializer;
    }

    public YmlCatalog FetchCatalog()
    {
        YmlCatalog catalog;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync(url).Result;

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