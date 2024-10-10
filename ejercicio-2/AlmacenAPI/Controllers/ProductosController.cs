using AlmacenAPI.Application.Commands;
using AlmacenAPI.Application.Queries;
using AlmacenAPI.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(CreateProductoCommand command)
        {
            var producto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _mediator.Send(new GetProductosQuery());
            return Ok(productos);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _mediator.Send(new GetProductoByIdQuery(id));
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }
    }
}
