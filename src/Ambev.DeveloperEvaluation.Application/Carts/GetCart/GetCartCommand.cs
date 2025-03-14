using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

public record GetCartCommand : IRequest<GetCartResult>
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
}