using MediatR;
using ThisRefactored.Application.Models;

namespace ThisRefactored.Application.Queries.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<ProductDto>;