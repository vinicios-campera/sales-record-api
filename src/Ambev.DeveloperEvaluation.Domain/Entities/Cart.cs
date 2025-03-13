using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Cart()
        {
            Date = DateTime.UtcNow;
        }

        public Guid UserId { get; set; }
        public User User { get; set; } = new();
       
        public DateTime Date { get; set; }

        public IEnumerable<CartProduct> CartProducts { get; set; }
    }
}