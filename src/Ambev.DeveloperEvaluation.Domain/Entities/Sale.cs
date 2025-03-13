using Ambev.DeveloperEvaluation.Domain.Attributes;
using Ambev.DeveloperEvaluation.Domain.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        public int Number { get; private set; }
        public void SetNumber(int number)
            => Number = number;

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Customer { get; set; } = string.Empty;
        public decimal TotalAmount { get; private set; }
        public decimal TotalDiscount { get; private set; }
        public decimal TotalSaleAmount { get; private set; }

        public void UpdateValuesAfterDiscount()
        {
            TotalAmount = Products.Sum(x => x.Total).Round();
            TotalDiscount = Products.Sum(x => x.Discount).Round();
            TotalSaleAmount = Products.Sum(x => x.SubTotal).Round();
        }

        public string Branch { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.String)]
        public Status Status { get; set; } = Status.Active;

        public IEnumerable<SaleProduct> Products { get; set; } = [];
    }

    public class SaleProduct
    {
        public string Product { get; set; } = string.Empty;

        [Discount(4, 9, 0.10)]
        [Discount(10, 20, 0.20)]
        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; private set; }
        public decimal SubTotal { get; private set; }

        [BsonRepresentation(BsonType.String)]
        public Status Status { get; set; } = Status.Active;

        public void SetTotal()
            => Total = (Quantity * Price).Round();

        public void SetDiscount(decimal discount)
        {
            Discount = discount.Round();
            SetSubTotal();
        }

        public void SetSubTotal()
            => SubTotal = (Total - Discount).Round();
    }

    public enum Status
    {
        Active = 0,
        Canceled,
    }
}