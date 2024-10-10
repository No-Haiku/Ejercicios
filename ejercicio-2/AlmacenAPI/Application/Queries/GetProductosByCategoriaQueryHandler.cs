// AlmacenAPI/Handlers/GetProductosByCategoriaQueryHandler.cs
using AlmacenAPI.Domain;
using AlmacenAPI.Queries;
using AlmacenAPI.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AlmacenAPI.Handlers
{
    public class GetProductosByCategoriaQueryHandler : IRequestHandler<GetProductosByCategoriaQuery, IEnumerable<Producto>>
    {
        private readonly IProductoRepository _productoRepository;

        public GetProductosByCategoriaQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<Producto>> Handle(GetProductosByCategoriaQuery request, CancellationToken cancellationToken)
        {
            return await _productoRepository.GetProductosByCategoriaAsync(request.CategoriaId);
        }
    }
}
