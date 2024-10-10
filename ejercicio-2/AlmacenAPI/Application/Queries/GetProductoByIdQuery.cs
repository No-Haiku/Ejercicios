using AlmacenAPI.Domain;
using MediatR;

namespace AlmacenAPI.Application.Queries
{
    public class GetProductoByIdQuery : IRequest<Producto>
    {
        public int Id { get; }

        public GetProductoByIdQuery(int id)
        {
            Id = id;
        }
    }
}
