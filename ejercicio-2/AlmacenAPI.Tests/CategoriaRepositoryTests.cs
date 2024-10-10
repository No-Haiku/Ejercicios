using AlmacenAPI.Domain;
using AlmacenAPI.Infrastructure;
using AlmacenAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AlmacenAPI.Tests
{
    public class CategoriaRepositoryTests
    {
        private readonly AlmacenDbContext _context;
        private readonly CategoriaRepository _repository;

        public CategoriaRepositoryTests()
        {
            // Cargar la configuración desde el archivo JSON
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json") 
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<AlmacenDbContext>()
                .UseSqlServer(connectionString) 
                .Options;

            _context = new AlmacenDbContext(options);
            _repository = new CategoriaRepository(connectionString, _context);
        }

        [Fact]
        public async Task AddCategoriaAsync_ShouldAddCategoria()
        {
            
            var categoria = new Categoria { Nombre = "Test", Descripcion = "Test Descripcion" };
            
            var addedCategoria = await _repository.AddCategoriaAsync(categoria);

            
            Assert.NotNull(addedCategoria);
            Assert.Equal(categoria.Nombre, addedCategoria.Nombre);
            Assert.Equal(categoria.Descripcion, addedCategoria.Descripcion);
        }

        [Fact]
        public async Task GetCategoriasAsync_ShouldReturnAllCategorias()
        {
            // Limpia la tabla Categorias
            _context.Database.ExecuteSqlRaw("DELETE FROM Categorias");
            await _context.SaveChangesAsync();

            
            var categorias = new List<Categoria>
            {
                new Categoria { Nombre = "Categoria1", Descripcion = "Descripcion1" },
                new Categoria { Nombre = "Categoria2", Descripcion = "Descripcion2" }
            };

            await _context.Categorias.AddRangeAsync(categorias);
            await _context.SaveChangesAsync();

           
            var result = await _repository.GetCategoriasAsync();

            
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCategoriaByIdAsync_ShouldReturnCategoria()
        {
            
            var categoria = new Categoria { Nombre = "Categoria1", Descripcion = "Descripcion1" };
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            
            var result = await _repository.GetCategoriaByIdAsync(categoria.Id);

            
            Assert.NotNull(result);
            Assert.Equal(categoria.Nombre, result.Nombre);
            Assert.Equal(categoria.Descripcion, result.Descripcion);
        }

        [Fact]
        public async Task GetCategoriaByIdAsync_ShouldReturnNull_WhenCategoriaDoesNotExist()
        {
            
            int nonExistentId = 999;

            
            var result = await _repository.GetCategoriaByIdAsync(nonExistentId);

            
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteCategoriaAsync_ShouldRemoveCategoria()
        {
            
            var categoria = new Categoria { Nombre = "Categoria1", Descripcion = "Descripcion1" };
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            
            await _repository.DeleteCategoriaAsync(categoria.Id);

            
            var deletedCategoria = await _context.Categorias.FindAsync(categoria.Id);
            Assert.Null(deletedCategoria);
        }

        [Fact]
        public async Task DeleteCategoriaAsync_ShouldNotThrow_WhenCategoriaDoesNotExist()
        {
            
            int nonExistentId = 999; // ID que no existe

            
            await _repository.DeleteCategoriaAsync(nonExistentId);

            
            Assert.True(true);
        }

        [Fact]
        public async Task UpdateCategoriaAsync_ShouldUpdateCategoria()
        {
           
            var categoria = new Categoria { Nombre = "Categoria1", Descripcion = "Descripcion1" };
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            
            categoria.Nombre = "Categoria Actualizada";
            var updatedCategoria = await _repository.UpdateCategoriaAsync(categoria);

            
            Assert.NotNull(updatedCategoria);
            Assert.Equal("Categoria Actualizada", updatedCategoria.Nombre);
        }

        [Fact]
        public async Task UpdateCategoriaAsync_ShouldThrowException_WhenCategoriaDoesNotExist()
        {
            
            var categoria = new Categoria { Id = 999, Nombre = "Categoria No Existente", Descripcion = "Descripcion" };

            
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
                await _repository.UpdateCategoriaAsync(categoria));
        }
    }
}
