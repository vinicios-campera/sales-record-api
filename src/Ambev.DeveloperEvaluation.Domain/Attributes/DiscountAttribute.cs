namespace Ambev.DeveloperEvaluation.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DiscountAttribute : Attribute
    {
        public int MinQuantity { get; }
        public int MaxQuantity { get; }
        public double DiscountPercentage { get; }

        public DiscountAttribute(int minQuantity, int maxQuantity, double discountPercentage)
        {
            MinQuantity = minQuantity;
            MaxQuantity = maxQuantity;
            DiscountPercentage = discountPercentage;
        }
    }
}