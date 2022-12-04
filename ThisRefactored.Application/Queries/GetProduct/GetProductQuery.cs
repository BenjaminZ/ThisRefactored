using MediatR;
using ThisRefactored.Application.Dtos;

namespace ThisRefactored.Application.Queries.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<ProductDto>;