using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductCategories
{
    public class GetProductsCategoriesCommand : IRequest<IEnumerable<string>>
    {
    }
}