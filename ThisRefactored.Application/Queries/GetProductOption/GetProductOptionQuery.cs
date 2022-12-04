using MediatR;
using ThisRefactored.Application.Dtos;

namespace ThisRefactored.Application.Queries.GetProductOption;

public record GetProductOptionQuery(Guid Id, Guid ProductId) : IRequest<ProductOptionDto>;