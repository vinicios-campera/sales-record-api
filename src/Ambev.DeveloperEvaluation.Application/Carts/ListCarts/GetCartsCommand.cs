using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class GetCartsCommand : IRequest<GetCartsResult>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Order { get; set; } = string.Empty;
    }
}