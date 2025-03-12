using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(user => user.Title).NotEmpty().Length(3, 100);
            RuleFor(user => user.Price).GreaterThan(0).PrecisionScale(18, 2, true);
            RuleFor(user => user.Description).NotEmpty().Length(3, 150);
            RuleFor(user => user.Category).NotNull();
            RuleFor(user => user.Image).NotEmpty().Must(BeAValidUrl).WithMessage("Invalid image url");
        }

        private bool BeAValidUrl(string url)
            => Uri.TryCreate(url, UriKind.Absolute, out _) && (url.StartsWith("http://") || url.StartsWith("https://"));
    }
}