using FluentValidation;
using ShopMvcApp_PD211.Entities;

namespace ShopMvcApp_PD211.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.ImageUrl)
                .Must(ValidateUri).WithMessage("Image URL must be a valid address.");
            RuleFor(x => x.Price)
                .GreaterThan(0);
            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100);
            RuleFor(x => x.Quantity)
               .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Description)
                .MaximumLength(100);
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category is required.");
        }

        public bool ValidateUri(string? uri)
        {
            // just so the validation passes if the uri is not required / nullable
            if (string.IsNullOrEmpty(uri))
            {
                return true;
            }
            return Uri.TryCreate(uri, UriKind.Absolute, out _);
        }
    }
}
