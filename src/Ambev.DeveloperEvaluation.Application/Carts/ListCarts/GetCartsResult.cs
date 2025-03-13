using Ambev.DeveloperEvaluation.Application.Carts.GetCart;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class GetCartsResult
    {
        public int TotalItems { get; set; }

        public IEnumerable<GetCartResult> Items { get; set; } = [];
    }
}