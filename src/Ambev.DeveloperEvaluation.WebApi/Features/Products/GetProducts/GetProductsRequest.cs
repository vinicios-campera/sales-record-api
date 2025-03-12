using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts
{
    public class GetProductsRequest : PaginatedRequestBase
    {
        [FromQuery(Name = "title")]
        public string? Title { get; set; }

        [FromQuery(Name = "category")]
        public ProductCategory? Category { get; set; }

        [FromQuery(Name = "_min")]
        public decimal? MinPrice { get; set; }

        [FromQuery(Name = "_max")]
        public decimal? MaxPrice { get; set; }
    }
}