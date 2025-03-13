namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<CreateProductResponse> Products { get; set; } = [];
    }

    public class CreateProductResponse
    {
        public Guid ProductId { get; set; }

        public decimal Quantity { get; set; }
    }
}