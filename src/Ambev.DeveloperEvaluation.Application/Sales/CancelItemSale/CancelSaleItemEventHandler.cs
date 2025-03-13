using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale
{
    public class CancelSaleItemEventHandler(ILogger<CancelSaleItemEventHandler> logger) : INotificationHandler<SaleItemCanceledEvent>
    {
        public Task Handle(SaleItemCanceledEvent notification, CancellationToken cancellationToken)
        {
            //O evento poderá ser publicado num message broken como Kafka, RabbitMq e etc... (não obrigatório)
            logger.LogInformation("Item {Product} da venda {Id} cancelada", notification.Product, notification.SaleId);
            return Task.CompletedTask;
        }
    }
}