using System.Diagnostics.CodeAnalysis;
using AllegroDealersBot.Models;

namespace AllegroDealersBot.Worker.Services;

internal class ProductModifiedComparator : IEqualityComparer<Offer>
{
    public bool Equals(Offer? x, Offer? y)
    {
        if (x == null || y == null)
        {
            return false;
        }

        return
            x.Id == y.Id &&
            (x.Price != y.Price ||
             !x.Name.Equals(y.Name) ||
             !x.Model.Equals(y.Model) ||
             !x.Vendor.Equals(y.Vendor) ||
             !x.VendorCode.Equals(y.VendorCode) ||
             x.StockQuantity != y.StockQuantity);
    }
    public int GetHashCode([DisallowNull] Offer obj)
    {
        return obj.Id.GetHashCode();
    }
}