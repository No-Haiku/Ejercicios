using AlmacenAPI.Application.Commands;
using AlmacenAPI.Application.Queries;
using AlmacenAPI.Commands;
using AlmacenAPI.Domain;
using AlmacenAPI.Queries;
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
        [HttpPut("{id}")]
        public async Task<ActionResult<Producto>> UpdateCategoria(int id, UpdateProductoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var updatedProducto = await _mediator.Send(command);
            return Ok(updatedProducto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var command = new DeleteProductoCommand { Id = id };

            // Envía el comando a través de Mediator
            await _mediator.Send(command);

            return NoContent(); // Retorna 204 No Content en caso de éxito
        }
        // Método para obtener productos por categoría
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosByCategoria(int categoriaId)
        {
            var query = new GetProductosByCategoriaQuery(categoriaId);
            var productos = await _mediator.Send(query);

            if (productos == null || !productos.Any())
            {
                return NotFound();
            }

            return Ok(productos);
        }
    }
}
