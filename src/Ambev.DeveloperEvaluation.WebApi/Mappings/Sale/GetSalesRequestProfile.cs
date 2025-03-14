using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sale
{
    public class GetSalesRequestProfile : Profile
    {
        public GetSalesRequestProfile()
        {
            CreateMap<CommonPaginatedRequest, GetSalesCommand>();
        }
    }
}