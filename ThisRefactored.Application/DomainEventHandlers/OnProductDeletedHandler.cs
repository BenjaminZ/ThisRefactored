using MediatR;
using Microsoft.EntityFrameworkCore;
using ThisRefactored.Domain.Events;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.DomainEventHandlers;

/// <summary>
///     We don't need this since ef core handles cascading deletes.
///     Just for demo purposes.
/// </summary>
public class OnProductDeletedHandler : INotificationHandler<DomainEventNotificationWrapper<OnProductDeleted>>
{
    private readonly ProductDbContext _dbContext;

    public OnProductDeletedHandler(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DomainEventNotificationWrapper<OnProductDeleted> notification, CancellationToken cancellationToken)
    {
        await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM ProductOptions WHERE ProductId = '{0}' collate nocase", notification.DomainEvent.ProductId);
    }
}