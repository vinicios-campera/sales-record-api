using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleUpdateEvent : INotification
    {
        public Guid Id { get; }
        public string Customer { get; }
        public decimal Amount { get; }

        public SaleUpdateEvent(Guid id, string customer, decimal amount)
        {
            Id = id;
            Customer = customer;
            Amount = amount;
        }
    }
}