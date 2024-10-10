using AlmacenAPI.Domain;

namespace AlmacenAPI.Repositories
{
    public interface IProductoRepository
    {
        Task<Producto> GetProductoByIdAsync(int id);
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task AddProductoAsync(Producto producto);
        Task UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(int id);
    }
}
