using TesteASPNet.Domain.Entity;
using FluentValidation;


namespace TesteASPNet.Service.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Description).MinimumLength(4)
                                       .WithMessage("A descrição deve conter mais do que 4 caracteres.");

            RuleFor(p => p.Description).MaximumLength(60)
                                       .WithMessage("A descrição deve conter entre 4 e 60 caracteres.");

            RuleFor(p => p.ExpirationDate.Date)
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("Data de validade não por ser menor ou igual a data atual.");

            RuleFor(p => p.ManufacturingDate)
            .LessThan(DateTime.Now)
            .WithMessage("Data de fabricação não por ser maior que a data atual.");

            RuleFor(p => p.ExpirationDate)
            .GreaterThan(p => p.ManufacturingDate)
            .WithMessage("Data de fabricação não por ser maior ou igual a data de validade.");

        }
    }
}


