using AlmacenAPI.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AlmacenAPI.Application.Commands
{
    public class DeleteProductoCommandHandler : IRequestHandler<DeleteProductoCommand>
    {
        private readonly IProductoRepository _productoRepository;

        public DeleteProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Unit> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {
            await _productoRepository.DeleteProductoAsync(request.Id);
            return Unit.Value; 
        }
    }
}
