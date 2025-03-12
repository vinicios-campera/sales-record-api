using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class GetProductsHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductsCommand, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsCommand request, CancellationToken cancellationToken)
        {
            var data = await productRepository.FilterAsync(request.Page, request.Size, request.Order, request.Title, request.Category, request.MinPrice, request.MaxPrice, cancellationToken);
            var result = new GetProductsResult { TotalItems = data.Item2, Items = mapper.Map<IEnumerable<GetProductResult>>(data.Item1) };
            return result;
        }
    }
}