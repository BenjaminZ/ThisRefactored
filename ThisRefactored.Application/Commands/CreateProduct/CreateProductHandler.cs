using MediatR;
using ThisRefactored.Application.Models;
using ThisRefactored.Domain.Entities;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly ProductDbContext _dbContext;

    public CreateProductHandler(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name,
                                  request.Description,
                                  request.Price,
                                  request.DeliveryPrice);
        
        var result = _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ProductDto.CreateFrom(result.Entity);
    }
}