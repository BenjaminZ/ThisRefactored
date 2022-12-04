using MediatR;
using Microsoft.EntityFrameworkCore;
using ThisRefactored.Application.Dtos;
using ThisRefactored.Application.Exceptions;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.Queries.GetProductOption;

public class GetProductOptionHandler : IRequestHandler<GetProductOptionQuery, ProductOptionDto>
{
    private readonly ProductDbContext _dbContext;

    public GetProductOptionHandler(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductOptionDto> Handle(GetProductOptionQuery request, CancellationToken cancellationToken)
    {
        var productOption = await _dbContext.ProductOptions.SingleOrDefaultAsync(x => x.Id == request.Id && x.ProductId == request.ProductId, cancellationToken);
        _ = productOption ?? throw new NotFoundException("Product option not found");
        return ProductOptionDto.CreateFrom(productOption);
    }
}