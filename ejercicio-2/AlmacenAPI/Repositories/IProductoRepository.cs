using System.Collections.Generic;
using System.Threading.Tasks;
using AlmacenAPI.Domain;

namespace AlmacenAPI.Repositories
{
    public interface IProductoRepository
    {
        // Operación de lectura (GET) - Usando Dapper
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int categoriaId);
        // Operaciones de escritura (INSERT, UPDATE, DELETE) - Usando ADO.NET o SQL directo
        Task<Producto> AddProductoAsync(Producto producto);
        Task<Producto> UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(int id);
    }
}
