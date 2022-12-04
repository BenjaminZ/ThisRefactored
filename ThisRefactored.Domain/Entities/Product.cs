using ThisRefactored.Domain.Events;

namespace ThisRefactored.Domain.Entities;

public class Product : Entity
{
    public Product(string name,
                   string description,
                   decimal price,
                   decimal deliveryPrice)
    {
        Id = NewIdGuid();
        Name = name;
        Description = description;
        Price = price;
        DeliveryPrice = deliveryPrice;
    }

    public Guid Id { get; init; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public decimal DeliveryPrice { get; set; }

    public IEnumerable<ProductOption> ProductOptions { get; init; } = new List<ProductOption>();

    public void Delete()
    {
        PublishDomainEvent(new OnProductDeleted(Id));
    }
}