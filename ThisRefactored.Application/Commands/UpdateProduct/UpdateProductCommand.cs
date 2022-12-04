using MediatR;
using ThisRefactored.Application.Dtos;

namespace ThisRefactored.Application.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id, UpdateProductCommandBody Body) : IRequest<ProductDto>;

public record UpdateProductCommandBody(string? Name,
                                       string? Description,
                                       decimal? Price,
                                       decimal? DeliveryPrice);