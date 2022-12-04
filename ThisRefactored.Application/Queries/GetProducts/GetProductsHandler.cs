using MediatR;
using Microsoft.EntityFrameworkCore;
using ThisRefactored.Application.Models;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.Queries.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
{
    private readonly ProductDbContext _dbContext;

    public GetProductsHandler(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.Page - 1) * request.PageSize;
        var take = request.PageSize;

        var totalCount = await _dbContext.Products.CountAsync(cancellationToken);
        var entities = await _dbContext.Products.OrderBy(x => x.Name)
                                       .Skip(skip)
                                       .Take(take)
                                       .ToListAsync(cancellationToken);

        return new PagedResult<ProductDto>(totalCount, entities.Select(ProductDto.CreateFrom));
    }
}