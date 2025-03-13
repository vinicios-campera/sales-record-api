using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleEventHandler(ILogger<CancelSaleEventHandler> logger) : INotificationHandler<SaleCanceledEvent>
    {
        public Task Handle(SaleCanceledEvent notification, CancellationToken cancellationToken)
        {
            //O evento poderá ser publicado num message broken como Kafka, RabbitMq e etc... (não obrigatório)
            logger.LogInformation("Venda cancelada - > Id: {Id}", notification.Id);
            return Task.CompletedTask;
        }
    }
}