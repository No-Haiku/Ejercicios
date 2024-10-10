using MediatR;
using System.Collections.Generic;
using AlmacenAPI.Domain;

namespace AlmacenAPI.Queries
{
    public class GetCategoriasQuery : IRequest<IEnumerable<Categoria>>
    {
    }
}
