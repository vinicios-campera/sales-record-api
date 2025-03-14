using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
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

            var sale = mapper.Map<Domain.Entities.Sale>(request);
            foreach (var item in sale.Products)
            {
                var command = new GetProductCommand { Id = item.ProductId };
                var response = await mediator.Send(command);
                item.Description = response.Description;
                item.CalculateDiscount();
            }
            sale.UpdateValuesAfterDiscount();

            var lastSaleNumber = await saleRepository.GetCurrentNumber(cancellationToken);
            sale.SetNumber(lastSaleNumber);

            await saleRepository.CreateAsync(sale, cancellationToken);
            await mediator.Publish(new SaleCreatedEvent(sale.Id, sale.Customer, sale.TotalAmount), cancellationToken);
            return mapper.Map<CreateSaleResult>(sale);
        }
    }
}