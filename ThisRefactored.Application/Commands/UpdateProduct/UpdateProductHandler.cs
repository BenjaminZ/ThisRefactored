using MediatR;
using Microsoft.EntityFrameworkCore;
using ThisRefactored.Application.Dtos;
using ThisRefactored.Application.Exceptions;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.Commands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly ProductDbContext _dbContext;

    public UpdateProductHandler(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        _ = product ?? throw new NotFoundException("Product not found");

        product.Name = request.Body.Name ?? product.Name;
        product.Description = request.Body.Description ?? product.Description;
        product.Price = request.Body.Price ?? product.Price;
        product.DeliveryPrice = request.Body.DeliveryPrice ?? product.DeliveryPrice;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return ProductDto.CreateFrom(product);
    }
}