using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductCategories
{
    public class GetProductsCategoriesHandler(IProductRepository productRepository) : IRequestHandler<GetProductsCategoriesCommand, IEnumerable<string>>
    {
        public async Task<IEnumerable<string>> Handle(GetProductsCategoriesCommand request, CancellationToken cancellationToken)
        {
            var data = await productRepository.GetCategoriesAsync();
            return data.ToList().ConvertAll(x => x.ToString());
        }
    }
}