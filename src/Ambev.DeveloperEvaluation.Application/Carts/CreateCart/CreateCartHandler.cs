using Ambev.DeveloperEvaluation.Application.Products.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartHandler(IUserRepository userRepository, IProductRepository productRepository, ICartRepository cartRepository) : IRequestHandler<CreateCartCommand, Guid>
    {
        public async Task<Guid> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCartCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = await userRepository.GetByIdAsync(request.UserId);
            var cartProducts = new List<CartProduct>();
            var cart = new Cart { User = user! };
            foreach (var product in request.Products)
            {
                var productDb = await productRepository.GetByIdAsync(product.ProductId);
                if (productDb is null)
                    throw new KeyNotFoundException($"Product with ID {product.ProductId} not found");

                var cartProduct = new CartProduct { Cart = cart, Product = productDb, Quantity = product.Quantity };
                cartProducts.Add(cartProduct);
            }

            cart.CartProducts = cartProducts;
            var createdCart = await cartRepository.CreateAsync(cart);
            return createdCart.Id;
        }
    }
}