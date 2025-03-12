using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(3, 100);
            RuleFor(x => x.Price).GreaterThan(0).PrecisionScale(18, 2, true);
            RuleFor(x => x.Description).NotEmpty().Length(3, 150);
            RuleFor(x => x.Category).NotNull();
            RuleFor(x => x.Image).NotEmpty().Must(BeAValidUrl).WithMessage("Invalid image url");
        }

        private bool BeAValidUrl(string url)
            => Uri.TryCreate(url, UriKind.Absolute, out _) && (url.StartsWith("http://") || url.StartsWith("https://"));
    }
}