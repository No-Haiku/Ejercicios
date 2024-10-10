using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlmacenAPI.Repositories;
using AlmacenAPI.Domain;
using AlmacenAPI.Queries;

namespace AlmacenAPI.Handlers
{
    public class GetCategoriasQueryHandler : IRequestHandler<GetCategoriasQuery, IEnumerable<Categoria>>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public GetCategoriasQueryHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Categoria>> Handle(GetCategoriasQuery request, CancellationToken cancellationToken)
        {
            return await _categoriaRepository.GetCategoriasAsync();
        }
    }
}
