using AlmacenAPI.Domain;
using AlmacenAPI.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AlmacenAPI.Application.Commands
{
    public class UpdateProductoCommandHandler : IRequestHandler<UpdateProductoCommand, Producto>
    {
        private readonly IProductoRepository _productoRepository;

        public UpdateProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Producto> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = await _productoRepository.GetProductoByIdAsync(request.Id);

            if (producto == null)
            {
                
                return null;
            }

            producto.Nombre = request.Nombre;
            producto.Precio = request.Precio;
            producto.CategoriaId = request.CategoriaId;
            producto.Cantidad = request.Cantidad;
            producto.Descripcion = request.Descripcion;
            return await _productoRepository.UpdateProductoAsync(producto);
        }
    }
}
