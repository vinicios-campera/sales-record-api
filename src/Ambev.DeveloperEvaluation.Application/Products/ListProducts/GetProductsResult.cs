using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class GetProductsResult
    {
        public int TotalItems { get; set; }

        public IEnumerable<GetProductResult> Items { get; set; } = [];
    }
}