using MediatR;
using Microsoft.EntityFrameworkCore;
using ThisRefactored.Application.Exceptions;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.Commands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly ProductDbContext _dbContext;

    public DeleteProductHandler(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        _ = product ?? throw new NotFoundException("Product not found");
        
        product.Delete();
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}