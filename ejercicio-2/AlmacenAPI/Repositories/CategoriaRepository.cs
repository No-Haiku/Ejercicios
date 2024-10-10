using AlmacenAPI.Domain;
using AlmacenAPI.Infrastructure;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmacenAPI.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _connectionString; // Para Dapper
        private readonly AlmacenDbContext _context; // Para EF Core 

        public CategoriaRepository(string connectionString, AlmacenDbContext context)
        {
            _connectionString = connectionString;
            _context = context;
        }

        // Operación de lectura usando Dapper
        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categorias WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Categoria>(query, new { Id = id });
            }
        }

        // Operación de lectura para obtener todas las categorías usando Dapper
        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categorias";
                return await connection.QueryAsync<Categoria>(query);
            }
        }

        // Método para agregar una categoría usando EF Core
        public async Task<Categoria> AddCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        // Método para actualizar una categoría usando EF Core
        public async Task<Categoria> UpdateCategoriaAsync(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return categoria;
        }

        // Método para eliminar una categoría usando EF Core
        public async Task DeleteCategoriaAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}
