// AlmacenAPI/Queries/GetProductosByCategoriaQuery.cs
using MediatR;
using System.Collections.Generic;
using AlmacenAPI.Domain;

namespace AlmacenAPI.Queries
{
    public class GetProductosByCategoriaQuery : IRequest<IEnumerable<Producto>>
    {
        public int CategoriaId { get; set; }

        public GetProductosByCategoriaQuery(int categoriaId)
        {
            CategoriaId = categoriaId;
        }
    }
}
