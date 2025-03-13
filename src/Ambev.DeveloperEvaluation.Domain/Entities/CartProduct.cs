using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class CartProduct : BaseEntity
    {
        public Guid CartId { get; set; }
        public Cart Cart { get; set; } = new();

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = new();

        public decimal Quantity { get; set; }
    }
}