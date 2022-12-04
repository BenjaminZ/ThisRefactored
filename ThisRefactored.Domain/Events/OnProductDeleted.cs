namespace ThisRefactored.Domain.Events;

public class OnProductDeleted : IDomainEvent
{
    public OnProductDeleted(Guid productId)
    {
        ProductId = productId;
    }

    public Guid ProductId { get; }
}