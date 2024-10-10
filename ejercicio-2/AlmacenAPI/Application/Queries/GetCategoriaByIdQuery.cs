using MediatR;
using AlmacenAPI.Domain;

namespace AlmacenAPI.Queries
{
    public class GetCategoriaByIdQuery : IRequest<Categoria>
    {
        public int Id { get; set; }

      
    }
}
