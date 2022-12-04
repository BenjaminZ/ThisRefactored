using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThisRefactored.Application.Commands.CreateProductOption;
using ThisRefactored.Application.Commands.DeleteProductOption;
using ThisRefactored.Application.Dtos;
using ThisRefactored.Application.Queries.GetProductOption;

namespace ThisRefactored.WebApi.Controllers;

[ApiController]
[Route("api/Products/{productId:guid}/[controller]")]
public class OptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductOptionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSingle(Guid productId, Guid id)
    {
        var productOption = await _mediator.Send(new GetProductOptionQuery(id, productId));
        return Ok(productOption);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductOptionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(Guid productId, [FromBody] CreateProductOptionCommandBody request)
    {
        var productOption = await _mediator.Send(new CreateProductOptionCommand(productId, request));
        return CreatedAtAction(nameof(GetSingle),
                               new
                               {
                                   id = productId,
                                   OptionId = productOption.Id
                               },
                               productOption);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid productId, Guid id)
    {
        await _mediator.Send(new DeleteProductOptionCommand(id, productId));
        return NoContent();
    }
}