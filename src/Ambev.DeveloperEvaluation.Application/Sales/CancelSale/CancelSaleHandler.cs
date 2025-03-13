using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler(ISaleRepository saleRepository, IMediator mediator) : IRequestHandler<CancelSaleCommand, bool>
    {
        public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await saleRepository.GetByIdAsync(request.Id);

            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            sale.Status = Status.Canceled;
            await saleRepository.UpdateAsync(sale);
            await mediator.Publish(new SaleCanceledEvent(sale.Id), cancellationToken);
            return true;
        }
    }
}