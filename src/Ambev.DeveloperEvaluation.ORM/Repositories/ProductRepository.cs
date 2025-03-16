using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository(DefaultContext context) : IProductRepository
{
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Products.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<(IEnumerable<Product?>, int)> FilterAsync(int page, int size, string? order, string? title, ProductCategory? category, decimal? minPrice, decimal? maxPrice, CancellationToken cancellationToken = default)
    {
        var query = context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(title))
        {
            if (title.StartsWith("*"))
                query = query.Where(p => p.Title.EndsWith(title.TrimStart('*')));
            else if (title.EndsWith("*"))
                query = query.Where(p => p.Title.StartsWith(title.TrimEnd('*')));
            else
                query = query.Where(p => p.Title == title);
        }

        if (category.HasValue)
            query = query.Where(p => p.Category == category);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        if (!string.IsNullOrEmpty(order))
        {
            var orderParams = order.Split(',');
            bool firstOrder = true;

            foreach (var orderParam in orderParams)
            {
                var parts = orderParam.Trim().Split(' ');
                var propertyName = parts[0];
                var descending = parts.Length > 1 && parts[1].ToLower() == "desc";

                query = query.ApplyOrdering(propertyName, descending, firstOrder);
                firstOrder = false;
            }
        }

        return (await query.Skip((page - 1) * size).Take(size).ToListAsync(), await query.CountAsync());
    }

    public async Task<IEnumerable<ProductCategory>> GetCategoriesAsync()
    {
        var data = await context.Products.Select(x => x.Category).GroupBy(x => x).Select(x => x.First()).ToListAsync();
        return data;
    }
}