using Ambev.DeveloperEvaluation.Domain.Attributes;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public static class DiscountApplicator
    {
        internal static void CalculateDiscount(this SaleProduct saleProduct)
        {
            var discountPercentage = saleProduct.GetPercentageDiscount();
            saleProduct.SetTotal();
            var discount = saleProduct.Total * discountPercentage;
            saleProduct.AddDiscount(discount);
        }

        private static decimal GetPercentageDiscount(this SaleProduct saleProduct)
        {
            var discountAttributes = saleProduct.GetType()
                .GetProperty(nameof(SaleProduct.Quantity))
                .GetCustomAttributes(typeof(DiscountAttribute), false)
                .Cast<DiscountAttribute>();

            double percentageDiscount = 0;

            var discount = discountAttributes
                .Where(d => saleProduct.Quantity >= d.MinQuantity && saleProduct.Quantity <= d.MaxQuantity);

            if (discount.Any())
                percentageDiscount = discount.Max(d => d.DiscountPercentage);

            return Convert.ToDecimal(percentageDiscount);
        }
    }
}