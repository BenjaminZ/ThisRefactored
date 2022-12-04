using FluentValidation;

namespace ThisRefactored.Application.Queries.GetProducts;

public class GetProductsValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, PagedQuery.MaxPageSize);
    }
}