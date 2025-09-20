using AllegroDealersBot.Models;

namespace AllegroDealersBot;

internal interface ICatalogFetcher
{
    YmlCatalog FetchCatalog();
}