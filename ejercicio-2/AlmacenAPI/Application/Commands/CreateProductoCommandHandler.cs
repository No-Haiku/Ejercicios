using AlmacenAPI.Domain;
using AlmacenAPI.Repositories;
using MediatR;

namespace AlmacenAPI.Application.Commands
{
    public class CreateProductoCommandHandler : IRequestHandler<CreateProductoCommand, Producto>
    {
        private readonly IProductoRepository _productoRepository;

        public CreateProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Producto> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = new Producto
            {
                Nombre = request.Nombre,
                Precio = request.Precio,
                Cantidad = request.Cantidad,
                Descripcion = request.Descripcion,
                CategoriaId = request.CategoriaId
            };

            await _productoRepository.AddProductoAsync(producto);
            return producto;
        }
    }
}
