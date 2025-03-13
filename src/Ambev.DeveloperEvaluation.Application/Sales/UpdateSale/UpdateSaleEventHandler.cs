using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleEventHandler(ILogger<UpdateSaleEventHandler> logger) : INotificationHandler<SaleUpdateEvent>
    {
        public Task Handle(SaleUpdateEvent notification, CancellationToken cancellationToken)
        {
            //O evento poderá ser publicado num message broken como Kafka, RabbitMq e etc... (não obrigatório)
            logger.LogInformation("Venda atualizada - > Id: {Id} Customer: {Customer} Amount: {Amount}",
                notification.Id,
                notification.Customer,
                notification.Amount);
            return Task.CompletedTask;
        }
    }
}