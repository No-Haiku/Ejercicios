using AlmacenAPI.Domain;
using AlmacenAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AlmacenAPI.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AlmacenDbContext _context;

        public ProductoRepository(AlmacenDbContext context)
        {
            _context = context;
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            return await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            return await _context.Productos.Include(p => p.Categoria).ToListAsync();
        }

        public async Task AddProductoAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductoAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
