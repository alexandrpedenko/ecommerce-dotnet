using Ecommerce.API.Contracts.Requests.Products;
using FluentValidation;

namespace Ecommerce.API.Validators.Products
{
    /// <summary>
    /// Update Product Validator
    /// </summary>
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequestDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}
