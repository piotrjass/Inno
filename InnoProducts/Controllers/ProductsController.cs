using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace InnoProducts.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using InnoProducts.Models;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _mediator.Send(new GetAllProductsQuery()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
        return result != null ? Ok(result) : NotFound();
        
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userId != null)
        {
            command.CreatorUserID = userId;  
        }
        else
        {
            
            return Unauthorized("User ID not found or invalid.");
        }
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProductById), new { id = result.ID }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Product ID mismatch.");
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User not authenticated.");
        }

        var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
        if (product == null)
        {
            return NotFound("Product not found.");
        }

        if (product.CreatorUserID != userId)
        {
            return Forbid("You are not authorized to update this product.");
        }

        return (await _mediator.Send(command)) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User not authenticated.");
        }

        var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
        if (product == null)
        {
            return NotFound("Product not found.");
        }

        if (product.CreatorUserID != userId)
        {
            return Forbid("You are not authorized to delete this product.");
        }

        return (await _mediator.Send(new DeleteProductCommand { Id = id })) ? NoContent() : NotFound();
    }
}