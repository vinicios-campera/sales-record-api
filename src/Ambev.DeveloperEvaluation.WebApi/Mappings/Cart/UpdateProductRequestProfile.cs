using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Cart
{
    public class UpdateCartRequestProfile : Profile
    {
        public UpdateCartRequestProfile()
        {
            CreateMap<UpdateCartRequest, UpdateCartCommand>();
            CreateMap<Features.Carts.UpdateCart.UpdateCartProduct, Application.Carts.UpdateCart.UpdateCartProduct>();

            CreateMap<UpdateCartRequest, UpdateCartResponse>();
            CreateMap<Features.Carts.UpdateCart.UpdateCartProduct, Features.Carts.UpdateCart.UpdateProductCartResponse>();
        }
    }
}