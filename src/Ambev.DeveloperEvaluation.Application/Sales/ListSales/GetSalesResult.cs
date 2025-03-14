using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class GetSalesResult
    {
        public int TotalItems { get; set; }

        public IEnumerable<GetSaleResult> Items { get; set; } = [];
    }
}