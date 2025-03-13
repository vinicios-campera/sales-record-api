using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IMediator mediator) : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var lastSaleNumber = await saleRepository.GetCurrentNumber(cancellationToken);
            var sale = mapper.Map<Domain.Entities.Sale>(request);
            sale.Products.ToList().ForEach(x => x.CalculateDiscount());
            sale.UpdateValuesAfterDiscount();
            sale.SetNumber(lastSaleNumber);

            await saleRepository.CreateAsync(sale, cancellationToken);
            await mediator.Publish(new SaleCreatedEvent(sale.Id, sale.Customer, sale.TotalAmount), cancellationToken);
            return mapper.Map<CreateSaleResult>(sale);
        }
    }
}