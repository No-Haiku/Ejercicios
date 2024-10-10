using MediatR;
using AlmacenAPI.Domain;

namespace AlmacenAPI.Commands
{
    public class UpdateCategoriaCommand : IRequest<Categoria>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
