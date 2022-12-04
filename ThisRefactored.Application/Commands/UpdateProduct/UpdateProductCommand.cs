using MediatR;
using ThisRefactored.Application.Models;

namespace ThisRefactored.Application.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id, UpdateProductCommandBody Body) : IRequest<ProductDto>;

public record UpdateProductCommandBody(string? Name,
                                       string? Description,
                                       decimal? Price,
                                       decimal? DeliveryPrice);