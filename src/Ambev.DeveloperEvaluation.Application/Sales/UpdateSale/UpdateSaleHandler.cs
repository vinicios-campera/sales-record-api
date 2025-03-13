using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IMediator mediator) : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var lastSaleNumber = await saleRepository.GetCurrentNumber(cancellationToken);
            var sale = mapper.Map<Domain.Entities.Sale>(request);
            sale.Products.ToList().ForEach(x => x.CalculateDiscount());
            sale.UpdateValuesAfterDiscount();

            await saleRepository.UpdateAsync(sale, cancellationToken);
            await mediator.Publish(new SaleUpdateEvent(sale.Id, sale.Customer, sale.TotalAmount), cancellationToken);
            return mapper.Map<UpdateSaleResult>(sale);
        }
    }
}