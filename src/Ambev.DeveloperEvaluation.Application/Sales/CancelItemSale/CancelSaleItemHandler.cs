using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale
{
    public class CancelSaleHandler(ISaleRepository saleRepository, IMediator mediator) : IRequestHandler<CancelSaleItemCommand, bool>
    {
        public async Task<bool> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
        {
            var sale = await saleRepository.GetByIdAsync(request.SaleId);

            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found");

            var updated = false;
            foreach (var item in sale.Products)
            {
                if (item.Product.Trim() == request.Product.Trim() && item.Status == Status.Active)
                {
                    item.Status = Status.Canceled;
                    updated = true;
                }
            }

            if (updated)
            {
                await saleRepository.UpdateAsync(sale);
                await mediator.Publish(new SaleItemCanceledEvent(sale.Id, request.Product), cancellationToken);
            }

            return updated;
        }
    }
}