using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sale
{
    public class GetSaleResultProfile : Profile
    {
        public GetSaleResultProfile()
        {
            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<GetSaleProductResult, GetSaleProductResponse>();
        }
    }
}