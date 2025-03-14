using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public record DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }
}