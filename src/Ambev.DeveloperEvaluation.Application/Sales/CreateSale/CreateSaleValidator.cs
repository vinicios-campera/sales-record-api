using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.Products).NotEmpty();

            RuleFor(x => x.Customer).NotEmpty().MinimumLength(5).MaximumLength(20);

            RuleFor(x => x.Branch).NotEmpty().MinimumLength(5).MaximumLength(20);

            RuleFor(x => x.Products).Must(x => x.GroupBy(p => p.ProductId).All(g => g.Count() <= 20))
                .WithMessage("Select only 20 products some");

            RuleFor(x => x.Products).Must(x => x.Any());
        }
    }
}