namespace ThisRefactored.Domain.Events;

public class ProductDeleted : IDomainEvent
{
    public ProductDeleted(Guid productId)
    {
        ProductId = productId;
    }

    public Guid ProductId { get; }
}