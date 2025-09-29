using AllegroDealersBot.Models;
using AllegroDealersBot.Worker.Interfaces;

namespace AllegroDealersBot.Worker.Services;

public class CatalogComparator : ICatalogComparator
{
    public List<Offer> CompareCatalogs(List<Offer> oldCatalog, List<Offer> currentCatalog)
    {
        var comparedOffers = new List<Offer>();
        comparedOffers.AddRange(GetNewProducts(oldCatalog, currentCatalog));
        comparedOffers.AddRange(GetModifiedProducts(oldCatalog, currentCatalog));
        comparedOffers.AddRange(GetRemovedProducts(oldCatalog, currentCatalog));
        comparedOffers.AddRange(GetNotChangedProducts(oldCatalog, currentCatalog));

        return comparedOffers;
    }

    private List<Offer> SetFieldStatus(List<Offer> offers, List<Offer> oldCatalog)
    {
        foreach (var offer in offers)
        {
            var oldOffer = oldCatalog.FirstOrDefault(i => i.Id == offer.Id);

            if (oldOffer != null)
            {
                offer.IsPriceChanged = oldOffer.Price != offer.Price;
                offer.IsStockChanged = oldOffer.StockQuantity != offer.StockQuantity;
            }
            else if (oldOffer == null)
            {
                offer.IsPriceChanged = true;
                offer.IsStockChanged = true;
            }

            if (offer.IsPriceChanged || IsStockAvailabilityChanged(offer, oldOffer))
            {
                offer.RequiresImmediateAttention = true;
            }
        }

        return offers;
    }

    private bool IsStockAvailabilityChanged(Offer newOffer, Offer oldOffer)
    {
        return newOffer.StockQuantity == 0 ||
               oldOffer.StockQuantity == 0 && newOffer.StockQuantity != oldOffer.StockQuantity;
    }

    private List<Offer> GetNewProducts(List<Offer> oldCatalog, List<Offer> currentCatalog)
    {
        var oldProductsIdHash = new HashSet<int>(oldCatalog.Select(i => i.Id));

        var newProducts = currentCatalog.Where(i => !oldProductsIdHash.Contains(i.Id)).ToList();

        newProducts.ForEach(i => i.Status = "NEW");

        return newProducts;
    }

    private List<Offer> GetRemovedProducts(List<Offer> oldCatalog, List<Offer> currentCatalog)
    {
        var currentProductsHash = new HashSet<int>(currentCatalog.Select(i => i.Id));

        var removedProducts = oldCatalog.Where(i => !currentProductsHash.Contains(i.Id)).ToList();

        removedProducts.ForEach(i => i.Status = "REMOVED");

        return removedProducts;
    }

    private List<Offer> GetModifiedProducts(List<Offer> oldCatalog, List<Offer> currentCatalog)
    {
        var modifiedOffers = currentCatalog.Intersect(oldCatalog, new ProductModifiedComparator()).ToList();

        modifiedOffers.ForEach(i => i.Status = "MODIFIED");

        return SetFieldStatus(modifiedOffers, oldCatalog).OrderByDescending(order => order.RequiresImmediateAttention)
            .ToList();
    }

    private List<Offer> GetNotChangedProducts(List<Offer> oldCatalog, List<Offer> currentCatalog)
    {
        var commonProducts = currentCatalog.Intersect(oldCatalog, new ProductNotModifiedComparator()).ToList();

        commonProducts.ForEach(i => i.Status = "UNCHANGED");

        return commonProducts;
    }
}