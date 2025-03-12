using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);

    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<(IEnumerable<Product?>, int)> FilterAsync(int page, int size, string? order, string? title, ProductCategory? category, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken = default);

    Task<IEnumerable<ProductCategory>> GetCategoriesAsync();
}