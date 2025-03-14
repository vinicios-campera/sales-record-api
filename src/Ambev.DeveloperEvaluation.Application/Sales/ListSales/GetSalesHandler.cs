using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class GetSalesHandler(ISaleRepository saleRepository, IMapper mapper) : IRequestHandler<GetSalesCommand, GetSalesResult>
    {
        public async Task<GetSalesResult> Handle(GetSalesCommand request, CancellationToken cancellationToken)
        {
            var data = await saleRepository.FilterAsync(request.Page, request.Size, request.Order, cancellationToken);
            var result = new GetSalesResult { TotalItems = data.Item2, Items = mapper.Map<IEnumerable<GetSaleResult>>(data.Item1) };
            return result;
        }
    }
}