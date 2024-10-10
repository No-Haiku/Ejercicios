using AlmacenAPI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlmacenAPI.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task<Categoria> AddCategoriaAsync(Categoria categoria);
        Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
        Task DeleteCategoriaAsync(int id);
    }
}
