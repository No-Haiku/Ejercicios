using AlmacenAPI.Domain;
using MediatR;

namespace AlmacenAPI.Application.Queries
{
    public class GetProductosQuery : IRequest<IEnumerable<Producto>> { }
}
