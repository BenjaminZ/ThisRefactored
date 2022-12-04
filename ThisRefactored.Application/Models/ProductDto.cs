﻿using ThisRefactored.Domain.Entities;

namespace ThisRefactored.Application.Models;

public record ProductDto(Guid Id,
                         string Name,
                         string Description,
                         decimal Price,
                         decimal DeliveryPrice)
{
    public static ProductDto CreateFrom(Product entity)
        => new ProductDto(entity.Id,
                          entity.Name,
                          entity.Description,
                          entity.Price,
                          entity.DeliveryPrice);
}