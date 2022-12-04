using MediatR;
using Microsoft.Extensions.Logging;
using ThisRefactored.Domain.Events;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.DomainEventHandlers;

/// <summary>
///     We don't need this since ef core handles cascading deletes.
///     Just for demo purposes.
/// </summary>
public class OnProductDeleteHandler : INotificationHandler<DomainEventNotificationWrapper<ProductDeleted>>
{
    private readonly ILogger<OnProductDeleteHandler> _logger;
    private readonly ProductDbContext _dbContext;

    public OnProductDeleteHandler(ILogger<OnProductDeleteHandler> logger, ProductDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public Task Handle(DomainEventNotificationWrapper<ProductDeleted> notification, CancellationToken cancellationToken)
    {
        _logger.LogDebug("OnProductDeleteHandler: {@Event}", notification.DomainEvent);
        return Task.CompletedTask;
    }
}