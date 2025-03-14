using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; }
        public string Customer { get; set; } = string.Empty;

        public string Branch { get; set; } = string.Empty;

        public IEnumerable<UpdateSaleCommandProducts> Products { get; set; } = [];

        public class UpdateSaleCommandProducts
        {
            public Guid ProductId { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}