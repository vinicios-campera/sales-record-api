using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using static Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.UpdateSaleCommand;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleCommand, Sale>();
            CreateMap<UpdateSaleCommandProducts, SaleProduct>();
            CreateMap<Sale, UpdateSaleResult>();
        }
    }
}