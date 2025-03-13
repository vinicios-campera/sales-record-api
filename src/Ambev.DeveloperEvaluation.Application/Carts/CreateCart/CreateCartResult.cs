namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartResult
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<CreateCartProductResult> CartProducts { get; set; } = [];
    }

    public class CreateCartProductResult
    {
        public Guid ProductId { get; set; }

        public decimal Quantity { get; set; }
    }
}