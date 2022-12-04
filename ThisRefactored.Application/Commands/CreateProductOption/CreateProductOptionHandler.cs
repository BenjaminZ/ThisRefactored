using MediatR;
using Microsoft.EntityFrameworkCore;
using ThisRefactored.Application.Dtos;
using ThisRefactored.Application.Exceptions;
using ThisRefactored.Domain.Entities;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.Commands.CreateProductOption;

public class CreateProductOptionHandler : IRequestHandler<CreateProductOptionCommand, ProductOptionDto>
{
    private readonly ProductDbContext _dbContext;

    public CreateProductOptionHandler(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductOptionDto> Handle(CreateProductOptionCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        _ = product ?? throw new NotFoundException("Product not found");

        var productOption = new ProductOption(product.Id,
                                              request.Body.Name,
                                              request.Body.Description);
        var result = _dbContext.ProductOptions.Add(productOption);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ProductOptionDto.CreateFrom(result.Entity);
    }
}