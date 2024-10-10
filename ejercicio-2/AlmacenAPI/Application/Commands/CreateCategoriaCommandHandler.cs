using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AlmacenAPI.Repositories;
using AlmacenAPI.Domain;
using AlmacenAPI.Commands;

namespace AlmacenAPI.Handlers
{
    public class CreateCategoriaCommandHandler : IRequestHandler<CreateCategoriaCommand, Categoria>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CreateCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Categoria> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = new Categoria
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };
            return await _categoriaRepository.AddCategoriaAsync(categoria);
        }
    }
}
