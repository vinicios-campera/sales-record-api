using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(x => x.Products).NotEmpty();

            RuleFor(x => x.Products).Must(x => x.GroupBy(p => p.ProductId).All(g => g.Count() == 1)).WithMessage("Duplicate productId existing");

            RuleForEach(x => x.Products).SetValidator(new CartProductValidator());
        }

        public class CartProductValidator : AbstractValidator<UpdateCartProduct>
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
}