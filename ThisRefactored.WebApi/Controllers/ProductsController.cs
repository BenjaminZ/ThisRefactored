using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThisRefactored.Application.Commands.CreateProduct;
using ThisRefactored.Application.Commands.DeleteProduct;
using ThisRefactored.Application.Commands.UpdateProduct;
using ThisRefactored.Application.Models;
using ThisRefactored.Application.Queries;
using ThisRefactored.Application.Queries.GetProduct;
using ThisRefactored.Application.Queries.GetProducts;

namespace ThisRefactored.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSingle(Guid id)
    {
        var product = await _mediator.Send(new GetProductQuery(id));
        return Ok(product);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetProductsQuery request)
    {
        var products = await _mediator.Send(request);
        return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] CreateProductCommand request)
    {
        var product = await _mediator.Send(request);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(Guid id, [FromBody] UpdateProductCommandBody request)
    {
        var product = await _mediator.Send(new UpdateProductCommand(id, request));
        return Ok(product);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}