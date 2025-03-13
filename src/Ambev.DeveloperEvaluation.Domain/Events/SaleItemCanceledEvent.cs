using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleItemCanceledEvent : INotification
    {
        public Guid SaleId { get; }

        public string Product { get; }

        public SaleItemCanceledEvent(Guid saleId, string product)
        {
            SaleId = saleId;
            Product = product;
        }
    }
}