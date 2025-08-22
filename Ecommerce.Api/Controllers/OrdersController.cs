using Ecommerce.API.DTOs.Orders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken ct)
        {
            // var result = await _mediator.Send(new CreateOrderCommand(...), ct);
            // return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            // var result = await _mediator.Send(new GetOrderByIdQuery(id), ct);
            // return Ok(result);
            return Ok(default(OrderResponse)); // placeholder
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            // var result = await _mediator.Send(new GetAllOrdersQuery(), ct);
            // return Ok(result);
            return Ok(Array.Empty<OrderResponse>()); // placeholder
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
        {
            // await _mediator.Send(new CancelOrderCommand(id), ct);
            return NoContent();
        }
    }
}
