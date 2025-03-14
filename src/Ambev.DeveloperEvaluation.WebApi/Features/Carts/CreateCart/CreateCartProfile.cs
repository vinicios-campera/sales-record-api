using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartProfile : Profile
    {
        public CreateCartProfile()
        {
            CreateMap<CreateCartRequest, CreateCartResponse>()
                .ForMember(dest => dest.Products, x => x.MapFrom((src, dest, target, ctx) => ctx.Mapper.Map<IEnumerable<CreateProductResponse>>(src.Products)));
            CreateMap<CreateCartProductRequest, CreateProductResponse>();
        }
    }
}