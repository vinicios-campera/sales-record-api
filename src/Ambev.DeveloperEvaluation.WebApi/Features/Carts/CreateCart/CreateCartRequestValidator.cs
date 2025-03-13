using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartRequestValidator()
        {
            RuleFor(x => x.Products).NotEmpty();

            RuleFor(x => x.Products).Must(x => x.GroupBy(p => p.ProductId).All(g => g.Count() == 1)).WithMessage("Duplicate productId existing");

            RuleForEach(x => x.Products).SetValidator(new CartProductValidator());
        }
    }

    public class CartProductValidator : AbstractValidator<CreateCartProduct>
    {
        public CartProductValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}