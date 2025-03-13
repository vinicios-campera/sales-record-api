namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

public class GetCartResult
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public IEnumerable<GetCartProductResult> CartProducts { get; set; } = [];
}

public class GetCartProductResult
{
    public Guid ProductId { get; set; }

    public decimal Quantity { get; set; }
}