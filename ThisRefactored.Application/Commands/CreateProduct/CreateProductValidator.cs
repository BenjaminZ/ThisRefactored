using FluentValidation;

namespace ThisRefactored.Application.Commands.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty();
        RuleFor(x => x.Description)
           .NotEmpty();
        RuleFor(x => x.Price)
           .PrecisionScale(31,
                           2,
                           true);
        RuleFor(x => x.DeliveryPrice)
           .PrecisionScale(31,
                           2,
                           true);
    }
}