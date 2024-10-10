using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AlmacenAPI.Repositories;
using AlmacenAPI.Domain;
using AlmacenAPI.Queries;

namespace AlmacenAPI.Handlers
{
    public class GetCategoriaByIdQueryHandler : IRequestHandler<GetCategoriaByIdQuery, Categoria>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public GetCategoriaByIdQueryHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Categoria> Handle(GetCategoriaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoriaRepository.GetCategoriaByIdAsync(request.Id);
        }
    }
}
