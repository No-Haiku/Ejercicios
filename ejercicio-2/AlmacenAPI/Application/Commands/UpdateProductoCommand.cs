using AlmacenAPI.Domain;
using MediatR;

namespace AlmacenAPI.Application.Commands
{
    public class UpdateProductoCommand : IRequest<Producto>
    {
        public int Id { get; }
        public string Nombre { get; }
        public decimal Precio { get; }
        public int CategoriaId { get; }
        public int Cantidad { get; }
        public string Descripcion { get; }
        public UpdateProductoCommand(int id, int cantidad, string nombre, decimal precio, int categoriaId, string descripcion)
        {   
            Id = id;
            Cantidad = cantidad;
            Nombre = nombre;
            Precio = precio;
            CategoriaId = categoriaId;
            Descripcion = descripcion;
        }
    }
}
