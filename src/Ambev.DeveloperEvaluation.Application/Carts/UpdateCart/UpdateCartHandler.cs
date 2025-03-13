using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartHandler(ICartRepository cartRepository, IMapper mapper) : IRequestHandler<UpdateCartCommand, UpdateCartResult>
    {
        public async Task<UpdateCartResult> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCartCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingCart = await cartRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingCart == null)
                throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

            var cart = mapper.Map(request, existingCart);

            var updatedProduct = await cartRepository.UpdateAsync(cart, cancellationToken);
            var result = mapper.Map<UpdateCartResult>(updatedProduct);
            return result;
        }
    }
}