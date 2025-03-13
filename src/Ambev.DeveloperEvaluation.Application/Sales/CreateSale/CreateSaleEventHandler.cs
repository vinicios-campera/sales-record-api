using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleEventHandler(ILogger<CreateSaleEventHandler> logger) : INotificationHandler<SaleCreatedEvent>
    {
        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            //O evento poderá ser publicado num message broken como Kafka, RabbitMq e etc... (não obrigatório)
            logger.LogInformation("Nova venda realizada - > Id: {Id} Customer: {Customer} Amount: {Amount}",
                notification.Id,
                notification.Customer,
                notification.Amount);
            return Task.CompletedTask;
        }
    }
}