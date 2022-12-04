using MediatR;
using Microsoft.EntityFrameworkCore;
using ThisRefactored.Application.Exceptions;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.Commands.DeleteProductOption;

public class DeleteProductOptionHandler : IRequestHandler<DeleteProductOptionCommand>
{
    private readonly ProductDbContext _dbContext;

    public DeleteProductOptionHandler(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteProductOptionCommand request, CancellationToken cancellationToken)
    {
        var productOption = await _dbContext.ProductOptions.SingleOrDefaultAsync(x => x.Id == request.Id && x.ProductId == request.ProductId, cancellationToken);
        _ = productOption ?? throw new NotFoundException("Product option not found");
        
        _dbContext.ProductOptions.Remove(productOption);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}