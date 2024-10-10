using AlmacenAPI.Domain;
using MediatR;

namespace AlmacenAPI.Application.Commands
{
    public class CreateProductoCommand : IRequest<Producto>
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
    }
}
