namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequest
    {
        public List<UpdateCartProduct> Products { get; set; } = [];
    }

    public class UpdateCartProduct
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}