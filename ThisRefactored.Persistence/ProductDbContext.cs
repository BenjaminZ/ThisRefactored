using Microsoft.EntityFrameworkCore;
using ThisRefactored.Domain;
using ThisRefactored.Domain.Entities;

namespace ThisRefactored.Persistence;

public class ProductDbContext : DbContext
{
    private readonly IDomainEventPublisher? _domainEventPublisher;

    public ProductDbContext(DbContextOptions<ProductDbContext> options, IDomainEventPublisher? domainEventPublisher = null) : base(options)
    {
        _domainEventPublisher = domainEventPublisher;
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductOption> ProductOptions => Set<ProductOption>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(cancellationToken);
        var res = await base.SaveChangesAsync(cancellationToken);
        return res;
    }

    private async Task DispatchDomainEvents(CancellationToken cancellationToken)
    {
        if (_domainEventPublisher == null)
        {
            return;
        }
        
        var domainEventEntities = ChangeTracker.Entries<IEntity>()
                                               .Select(po => po.Entity)
                                               .Where(po => po.DomainEvents.Any())
                                               .ToArray();

        foreach (var entity in domainEventEntities)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                await _domainEventPublisher.PublishDomainEventAsync(domainEvent, cancellationToken);
            }
            
            entity.ClearEvents();
        }
    }
}