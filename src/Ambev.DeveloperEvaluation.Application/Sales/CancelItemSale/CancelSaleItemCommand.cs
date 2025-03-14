using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale
{
    public class CancelSaleItemCommand : IRequest<bool>
    {
        public Guid SaleId { get; set; }

        public Guid ProductId { get; set; }
    }
}