using FluentValidation;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly List<string> ValidCategories = ["Electronics", "Fashion", "Mobiles", "Appliances", "Health"];

        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product ID is required.");

            RuleFor(x => x.Name)
            .Length(3, 100).WithMessage("Product name must be between 3 and 100 characters.")
            .Matches("^[a-zA-Z0-9 ]+$").WithMessage("Product name can only contain letters, numbers, and spaces.")
            .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Category)
                .Must(category => category != null && ValidCategories.Contains(category))
                .WithMessage("Invalid product category.")
                 .When(x => !string.IsNullOrEmpty(x.Category));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.")
                .PrecisionScale(18, 2, false).WithMessage("Price cannot have more than 2 decimal places.")
                .When(x => x.Price != default);
        }

    }
}
