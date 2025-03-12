using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingProduct = await productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {request.Id} not found");

            var product = mapper.Map(request, existingProduct);

            var updatedProduct = await productRepository.UpdateAsync(product, cancellationToken);
            var result = mapper.Map<UpdateProductResult>(updatedProduct);
            return result;
        }
    }
}