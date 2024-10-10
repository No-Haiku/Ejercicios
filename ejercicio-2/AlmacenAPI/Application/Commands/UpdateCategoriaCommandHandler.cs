using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AlmacenAPI.Repositories;
using AlmacenAPI.Domain;
using AlmacenAPI.Commands;

namespace AlmacenAPI.Handlers
{
    public class UpdateCategoriaCommandHandler : IRequestHandler<UpdateCategoriaCommand, Categoria>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public UpdateCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Categoria> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = new Categoria
            {
                Id = request.Id,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };
            return await _categoriaRepository.UpdateCategoriaAsync(categoria);
        }
    }
}
