using MediatR;
using ThisRefactored.Application.Dtos;

namespace ThisRefactored.Application.Commands.CreateProduct;

public record CreateProductCommand(string Name,
                                   string Description = "",
                                   decimal Price = 0,
                                   decimal DeliveryPrice = 0) : IRequest<ProductDto>;