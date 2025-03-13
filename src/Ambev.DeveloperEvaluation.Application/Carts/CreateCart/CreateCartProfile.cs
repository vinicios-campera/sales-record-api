using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartProfile : Profile
    {
        public CreateCartProfile()
        {
            CreateMap<Domain.Entities.Cart, CreateCartResult>();
            CreateMap<Domain.Entities.CartProduct, CreateCartProductResult>();
        }
    }
}