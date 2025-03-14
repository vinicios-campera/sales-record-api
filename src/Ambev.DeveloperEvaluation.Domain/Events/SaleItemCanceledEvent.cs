using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleItemCanceledEvent : INotification
    {
        public Guid SaleId { get; }

        public Guid ProductId { get; set; }

        public SaleItemCanceledEvent(Guid saleId,  Guid productId)
        {
            SaleId = saleId;
            ProductId = productId;
        }
    }
}