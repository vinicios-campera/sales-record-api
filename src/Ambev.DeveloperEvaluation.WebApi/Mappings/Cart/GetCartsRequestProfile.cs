using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Cart
{
    public class GetCartsRequestProfile : Profile
    {
        public GetCartsRequestProfile()
        {
            CreateMap<CommonPaginatedRequest, GetCartsCommand>();
        }
    }
}
