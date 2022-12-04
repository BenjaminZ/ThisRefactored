using ThisRefactored.Domain.Entities;

namespace ThisRefactored.Application.Dtos;

public record ProductOptionDto(Guid Id,
                               Guid ProductId,
                               string Name,
                               string Description)
{
    public static ProductOptionDto CreateFrom(ProductOption entity)
        => new(entity.Id,
               entity.ProductId,
               entity.Name,
               entity.Description);
}