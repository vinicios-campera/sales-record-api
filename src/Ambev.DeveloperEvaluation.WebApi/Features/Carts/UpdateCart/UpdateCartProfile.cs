using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartResult, UpdateCartResponse>()
                .ForMember(dest => dest.Products, x => x.MapFrom((src, dest, target, ctx) => ctx.Mapper.Map<IEnumerable<UpdateProductResponse>>(src.CartProducts)));
            CreateMap<UpdateCartProductResult, UpdateProductResponse>();
        }
    }
}