using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string Customer { get; set; } = string.Empty;

        public string Branch { get; set; } = string.Empty;

        public IEnumerable<CreateSaleCommandProducts> Products { get; set; } = [];

        public class CreateSaleCommandProducts
        {
            public string Product { get; set; } = string.Empty;
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}