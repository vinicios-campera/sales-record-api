using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleResponse
    {
        public Guid Id { get; set; }
        public int Number { get;  set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Customer { get; set; } = string.Empty;
        public decimal TotalAmount { get;  set; }
        public decimal TotalDiscount { get;  set; }
        public decimal TotalSaleAmount { get;  set; }
        public string Branch { get; set; } = string.Empty;
        public IEnumerable<GetSaleProductResponse> Products { get; set; } = [];
    }

    public class GetSaleProductResponse
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
