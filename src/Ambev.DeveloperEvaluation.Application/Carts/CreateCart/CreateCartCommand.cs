using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public List<CreateCartProduct> Products { get; set; } = [];
    }

    public class CreateCartProduct
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}