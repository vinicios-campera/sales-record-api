using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleResult
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Customer { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public IEnumerable<GetSaleProductResult> Products { get; set; } = [];
    }

    public class GetSaleProductResult
    {
        public Guid ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; private set; }
        public decimal SubTotal { get; private set; }
        public Status Status { get; set; }
    }
}