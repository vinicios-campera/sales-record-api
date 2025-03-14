namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequest
    {
        public List<CreateCartProductRequest> Products { get; set; } = [];
    }

    public class CreateCartProductRequest
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}