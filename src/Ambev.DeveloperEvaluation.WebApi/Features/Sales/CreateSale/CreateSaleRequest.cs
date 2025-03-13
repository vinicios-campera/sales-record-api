namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public string Customer { get; set; } = string.Empty;

        public string Branch { get; set; } = string.Empty;

        public IEnumerable<CreateSaleProductRequest> Products { get; set; } = [];
    }

    public class CreateSaleProductRequest
    {
        public string Product { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}