using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCanceledEvent : INotification
    {
        public Guid Id { get; }

        public SaleCanceledEvent(Guid id)
        {
            Id = id;
        }
    }
}