using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public List<UpdateCartProduct> Products { get; set; } = [];
    }

    public class UpdateCartProduct
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}