using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class GetProductsCommand : IRequest<GetProductsResult>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Order { get; set; } = string.Empty;
        public string? Title { get; set; } = string.Empty;
        public ProductCategory? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}