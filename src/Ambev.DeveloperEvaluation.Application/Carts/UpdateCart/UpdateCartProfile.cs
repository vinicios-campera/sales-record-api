using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartCommand, Cart>()
                .ForMember(dest => dest.CartProducts, x => x.MapFrom((src, dest, target, ctx) => ctx.Mapper.Map<IEnumerable<UpdateCartProduct>>(src.Products)));
            CreateMap<UpdateCartProduct, CartProduct>();
        }
    }
}