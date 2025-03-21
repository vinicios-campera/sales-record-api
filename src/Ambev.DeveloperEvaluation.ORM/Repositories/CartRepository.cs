﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CartRepository(DefaultContext context) : ICartRepository
{
    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await context.Carts.AddAsync(cart, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        context.Carts.Update(cart);
        await context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Carts.Include(x => x.CartProducts).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return false;

        context.Carts.Remove(cart);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<(IEnumerable<Cart?>, int)> FilterAsync(Guid userId, int page, int size, string? order, CancellationToken cancellationToken = default)
    {
        var query = context.Carts.AsQueryable();

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

        return (await query.Skip((page - 1) * size).Take(size).Include(x => x.CartProducts).Where(x => x.UserId == userId).ToListAsync(), await query.CountAsync());
    }
}