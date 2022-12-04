using MediatR;
using ThisRefactored.Application.Models;

namespace ThisRefactored.Application.Queries.GetProducts;

public record GetProductsQuery(int Page, int PageSize) : PagedQuery(Page, PageSize), IRequest<PagedResult<ProductDto>>;