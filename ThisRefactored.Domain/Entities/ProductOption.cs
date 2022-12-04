namespace ThisRefactored.Domain.Entities;

public class ProductOption : Entity
{
    public ProductOption(Guid productId,
                         string name,
                         string description)
    {
        Id = NewIdGuid();
        ProductId = productId;
        Name = name;
        Description = description;
    }

    public Guid Id { get; init; }

    public Guid ProductId { get; init; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Product Product { get; init; } = null!;
}