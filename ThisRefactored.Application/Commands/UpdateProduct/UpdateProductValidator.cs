using FluentValidation;

namespace ThisRefactored.Application.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommandBody>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Price)
           .PrecisionScale(31,
                           2,
                           true)
           .When(x => x.Price != null);
        RuleFor(x => x.DeliveryPrice)
           .PrecisionScale(31,
                           2,
                           true)
           .When(x => x.DeliveryPrice != null);
    }
}