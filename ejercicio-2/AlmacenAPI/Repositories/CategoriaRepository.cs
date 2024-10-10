using AlmacenAPI.Domain;
using AlmacenAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmacenAPI.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AlmacenDbContext _context;

        public CategoriaRepository(AlmacenDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            return await _context.Categorias
                .Include(c => c.Productos) // Incluir productos si es necesario
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            return await _context.Categorias
                .Include(c => c.Productos) // Incluir productos si es necesario
                .ToListAsync();
        }

        public async Task<Categoria> AddCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> UpdateCategoriaAsync(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return categoria;
        }

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
