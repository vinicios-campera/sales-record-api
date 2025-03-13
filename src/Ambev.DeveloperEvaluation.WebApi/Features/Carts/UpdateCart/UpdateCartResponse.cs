namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<UpdateProductResponse> Products { get; set; } = [];
    }

    public class UpdateProductResponse
    {
        public Guid ProductId { get; set; }

        public decimal Quantity { get; set; }
    }
}