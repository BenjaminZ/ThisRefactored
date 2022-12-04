using MediatR;
using ThisRefactored.Application.Dtos;

namespace ThisRefactored.Application.Commands.CreateProductOption;

public record CreateProductOptionCommand(Guid ProductId, CreateProductOptionCommandBody Body) : IRequest<ProductOptionDto>;

public record CreateProductOptionCommandBody(string Name, string Description);