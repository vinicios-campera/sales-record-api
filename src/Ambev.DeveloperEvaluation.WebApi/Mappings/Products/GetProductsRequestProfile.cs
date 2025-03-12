using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Products
{
    public class GetProductsRequestProfile : Profile
    {
        public GetProductsRequestProfile()
        {
            CreateMap<GetProductsRequest, GetProductsCommand>();
            CreateMap<PaginatedRequestBase, GetProductsCommand>();
        }
    }
}