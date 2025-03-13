namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public string Customer { get; set; } = string.Empty;

        public string Branch { get; set; } = string.Empty;

        public IEnumerable<UpdateSaleProductRequest> Products { get; set; } = [];
    }

    public class UpdateSaleProductRequest
    {
        public string Product { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}