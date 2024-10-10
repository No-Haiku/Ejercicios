using AlmacenAPI.Domain;
using AlmacenAPI.Infrastructure;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AlmacenAPI.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString; // Para Dapper
        private readonly AlmacenDbContext _context; // Para EF Core 

        public ProductoRepository(string connectionString, AlmacenDbContext context)
        {
            _connectionString = connectionString;
            _context = context;
        }

            // Método para obtener todos los productos usando Dapper 
            public async Task<IEnumerable<Producto>> GetProductosAsync()
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM Productos";
                    return await db.QueryAsync<Producto>(sql);
                }
            }
        // Método para obtener productos por categoría usando Dapper
        public async Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int categoriaId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Productos WHERE CategoriaId = @CategoriaId";
                return await db.QueryAsync<Producto>(sql, new { CategoriaId = categoriaId });
            }
        }
        // Método para obtener un producto por su Id usando Dapper 
        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Productos WHERE Id = @Id";
                return await db.QuerySingleOrDefaultAsync<Producto>(sql, new { Id = id });
            }
        }

        // Método para agregar un producto usando EF Core
        public async Task<Producto> AddProductoAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        // Método para actualizar un producto usando EF Core
        public async Task<Producto> UpdateProductoAsync(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return producto;
        }

        // Método para eliminar un producto usando EF Core
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
