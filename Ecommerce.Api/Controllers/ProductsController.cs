using Ecommerce.API.DTOs.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            // var result = await _mediator.Send(new GetAllProductsQuery(), ct);
            // return Ok(result);
            return Ok(Array.Empty<ProductResponse>()); // placeholder
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            // var result = await _mediator.Send(new GetProductByIdQuery(id), ct);
            // return Ok(result);
            return Ok(default(ProductResponse)); // placeholder
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateProductRequest request, CancellationToken ct)
        {
            // var result = await _mediator.Send(new CreateProductCommand(request...), ct);
            // return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, UpdateProductRequest request, CancellationToken ct)
        {
            if (id != request.Id) return BadRequest("Route id and body id mismatch.");
            // await _mediator.Send(new UpdateProductCommand(...), ct);
            return NoContent();
        }
    }
}
