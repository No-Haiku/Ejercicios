using AlmacenAPI.Domain;
using AlmacenAPI.Repositories;
using MediatR;

namespace AlmacenAPI.Application.Queries
{
    public class GetProductosQueryHandler : IRequestHandler<GetProductosQuery, IEnumerable<Producto>>
    {
        private readonly IProductoRepository _productoRepository;

        public GetProductosQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<Producto>> Handle(GetProductosQuery request, CancellationToken cancellationToken)
        {
            return await _productoRepository.GetProductosAsync();
        }
    }
}
