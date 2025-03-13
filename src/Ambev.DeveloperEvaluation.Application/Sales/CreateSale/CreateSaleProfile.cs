using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using static Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateSaleCommand;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>();
            CreateMap<CreateSaleCommandProducts, SaleProduct>();
            CreateMap<Sale, CreateSaleResult>();
        }
    }
}