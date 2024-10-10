using AlmacenAPI.Domain;
using AlmacenAPI.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AlmacenAPI.Application.Queries
{
    public class GetProductoByIdQueryHandler : IRequestHandler<GetProductoByIdQuery, Producto>
    {
        private readonly IProductoRepository _productoRepository;

        public GetProductoByIdQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Producto> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productoRepository.GetProductoByIdAsync(request.Id);
        }
    }
}
