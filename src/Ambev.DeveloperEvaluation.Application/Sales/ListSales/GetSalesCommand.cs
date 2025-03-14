using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class GetSalesCommand : IRequest<GetSalesResult>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Order { get; set; } = string.Empty;
    }
}