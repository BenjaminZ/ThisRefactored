using MediatR;
using ThisRefactored.Application.Dtos;

namespace ThisRefactored.Application.Queries.GetProducts;

public record GetProductsQuery(int Page, int PageSize) : PagedQuery(Page, PageSize), IRequest<PagedResult<ProductDto>>;