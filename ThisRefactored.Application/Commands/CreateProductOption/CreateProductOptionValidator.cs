using FluentValidation;

namespace ThisRefactored.Application.Commands.CreateProductOption;

public class CreateProductOptionValidator : AbstractValidator<CreateProductOptionCommandBody>
{
    public CreateProductOptionValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty();
        RuleFor(x => x.Description)
           .NotEmpty();
    }
}