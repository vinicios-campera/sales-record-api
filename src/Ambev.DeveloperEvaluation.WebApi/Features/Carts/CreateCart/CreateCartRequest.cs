namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequest
    {
        public List<CreateCartProduct> Products { get; set; } = [];
    }

    public class CreateCartProduct
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}