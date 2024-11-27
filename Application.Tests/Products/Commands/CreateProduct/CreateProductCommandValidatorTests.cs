using FluentValidation.TestHelper;

namespace Application.Products.Commands.CreateProduct.Tests
{
    public class CreateProductCommandValidatorTests
    {
        [Fact()]
        public void CreateProductCommandValidatorTest()
        {
            // arrange

            var command = new CreateProductCommand()
            {
                Name = "Test",
                Category = "Electronics",
                Description = "test",
                Price = 1245,
                StockAvailable = 100
            };

            var validator = new CreateProductCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            // arrange

            var command = new CreateProductCommand()
            {
                Name = "Te",
                Category = "Test",
                Price = 0,
                StockAvailable = -10
            };

            var validator = new CreateProductCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.Price);
            result.ShouldHaveValidationErrorFor(c => c.StockAvailable);
        }

        [Theory()]
        [InlineData("Electronics")]
        [InlineData("Fashion")]
        [InlineData("Mobiles")]
        [InlineData("Appliances")]
        [InlineData("Health")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            // arrange
            var validator = new CreateProductCommandValidator();
            var command = new CreateProductCommand { Category = category };

            // act

            var result = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);

        }
    }
}