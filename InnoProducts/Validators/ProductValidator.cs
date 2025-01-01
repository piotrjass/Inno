using FluentValidation;
using InnoProducts.Models;

namespace InnoProducts.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            // Walidacja nazwy produktu: musi być niepusta i nie dłuższa niż 100 znaków
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(1, 100).WithMessage("Product name must be between 1 and 100 characters.");

            // Walidacja ceny: cena musi być większa od 0
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            // Walidacja dostępności: nie może być null (czyli boolean musi być określony)
            RuleFor(p => p.Availability)
                .NotNull().WithMessage("Stock status is required.");

            // Walidacja daty stworzenia: data nie może być w przyszłości
            RuleFor(p => p.CreationDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future.");
        }
    }
}