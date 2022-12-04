using MediatR;
using Microsoft.EntityFrameworkCore;
using ThisRefactored.Application.Exceptions;
using ThisRefactored.Application.Models;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
{
    private readonly ProductDbContext _dbContext;

    public GetProductQueryHandler(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        return ProductDto.CreateFrom(product);
    }
}