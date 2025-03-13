namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

public class GetCartResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public DateTime Date { get; set; }

    public IEnumerable<GetCartProductResponse> Products { get; set; } = [];
}

public class GetCartProductResponse
{
    public Guid ProductId { get; set; }

    public decimal Quantity { get; set; }
}