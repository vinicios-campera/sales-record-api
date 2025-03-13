using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using static Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.UpdateSaleCommand;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sale
{
    public class UpdateSaleRequestProfile : Profile
    {
        public UpdateSaleRequestProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleProductRequest, UpdateSaleCommandProducts>();
        }
    }
}