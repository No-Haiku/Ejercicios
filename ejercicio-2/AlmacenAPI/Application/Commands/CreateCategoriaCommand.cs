using MediatR;
using AlmacenAPI.Domain;

namespace AlmacenAPI.Commands
{
    public class CreateCategoriaCommand : IRequest<Categoria>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
