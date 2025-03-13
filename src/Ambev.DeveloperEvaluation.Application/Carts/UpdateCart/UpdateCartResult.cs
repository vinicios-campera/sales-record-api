namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartResult
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<UpdateCartProductResult> CartProducts { get; set; } = [];
    }

    public class UpdateCartProductResult
    {
        public Guid ProductId { get; set; }

        public decimal Quantity { get; set; }
    }
}