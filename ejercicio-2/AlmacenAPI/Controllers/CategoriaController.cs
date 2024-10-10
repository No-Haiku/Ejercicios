using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlmacenAPI.Commands;
using AlmacenAPI.Queries;
using AlmacenAPI.Domain;

namespace AlmacenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            var query = new GetCategoriasQuery();
            var categorias = await _mediator.Send(query);
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var query = new GetCategoriaByIdQuery { Id = id };
            var categoria = await _mediator.Send(query);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateCategoria(CreateCategoriaCommand command)
        {
            var categoria = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Categoria>> UpdateCategoria(int id, UpdateCategoriaCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var updatedCategoria = await _mediator.Send(command);
            return Ok(updatedCategoria);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var command = new DeleteCategoriaCommand { Id = id };

            // Envía el comando a través de Mediator
            await _mediator.Send(command);

            return NoContent(); // Retorna 204 No Content en caso de éxito
        }
    }
}
