using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

public record GetCartCommand : IRequest<GetCartResult>
{
    public Guid Id { get; }

    public GetCartCommand(Guid id)
    {
        Id = id;
    }
}