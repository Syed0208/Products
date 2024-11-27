using FluentValidation;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly List<string> ValidCategories = ["Electronics", "Fashion", "Mobiles", "Appliances", "Health"];

        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(3, 100).WithMessage("Product name must be between 3 and 100 characters.")
            .Matches("^[a-zA-Z0-9 ]+$").WithMessage("Product name can only contain letters, numbers, and spaces.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Product category is required.")
                .Must(category => ValidCategories.Contains(category))
                .WithMessage("Invalid product category.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.")
                .PrecisionScale(18, 2, false).WithMessage("Price cannot have more than 2 decimal places.");

            RuleFor(x => x.StockAvailable)
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to zero.");

        }

    }
}
