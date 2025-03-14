using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public record DeleteCartCommand : IRequest
{
    public Guid Id { get; set; }
}