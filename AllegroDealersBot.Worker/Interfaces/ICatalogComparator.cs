using AllegroDealersBot.Models;

namespace AllegroDealersBot.Worker.Interfaces;

internal interface ICatalogComparator
{
    public List<Offer> CompareCatalogs(List<Offer> oldCatalog, List<Offer> currentCatalog);
}