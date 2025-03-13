using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Cart
{
    public class GetCartResultProfile : Profile
    {
        public GetCartResultProfile()
        {
            CreateMap<GetCartResult, GetCartResponse>()
                .ForMember(dest => dest.Products, x => x.MapFrom((src, dest, target, ctx) => ctx.Mapper.Map<IEnumerable<GetCartProductResponse>>(src.CartProducts)));
            CreateMap<GetCartProductResult, GetCartProductResponse>();
        }
    }
}