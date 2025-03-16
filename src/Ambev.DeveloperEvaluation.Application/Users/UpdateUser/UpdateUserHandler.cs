using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    public class UpdateUserHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<UpdateUserCommand, Guid>
    {
        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingProduct = await userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingProduct == null)
                throw new KeyNotFoundException($"User with ID {request.Id} not found");

            var user = mapper.Map(request, existingProduct);

            var updatedProduct = await userRepository.UpdateAsync(user, cancellationToken);
            return updatedProduct.Id;
        }
    }
}