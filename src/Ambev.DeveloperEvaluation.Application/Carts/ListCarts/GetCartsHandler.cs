using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class GetCartsHandler(ICartRepository cartRepository, IMapper mapper) : IRequestHandler<GetCartsCommand, GetCartsResult>
    {
        public async Task<GetCartsResult> Handle(GetCartsCommand request, CancellationToken cancellationToken)
        {
            var data = await cartRepository.FilterAsync(request.UserId, request.Page, request.Size, request.Order, cancellationToken);
            var result = new GetCartsResult { TotalItems = data.Item2, Items = mapper.Map<IEnumerable<GetCartResult>>(data.Item1) };
            return result;
        }
    }
}