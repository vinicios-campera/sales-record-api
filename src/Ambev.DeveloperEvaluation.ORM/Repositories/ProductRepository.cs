using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
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

                query = ApplyOrdering(query, propertyName, descending, firstOrder);
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

    private IQueryable<Product> ApplyOrdering(IQueryable<Product> query, string propertyName, bool descending, bool firstOrder)
    {
        var parameter = Expression.Parameter(typeof(Product), "p");
        var property = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);

        string methodName = firstOrder ?
            (descending ? "OrderByDescending" : "OrderBy") :
            (descending ? "ThenByDescending" : "ThenBy");

        var resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(Product), property.Type }, query.Expression, Expression.Quote(lambda));
        return query.Provider.CreateQuery<Product>(resultExp);
    }
}