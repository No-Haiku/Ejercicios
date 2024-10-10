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
                .SetBasePath(Directory.GetCurrentDirectory()) // Asegúrate de que la ruta es correcta
                .AddJsonFile("appsettings.json") // Cargar el archivo de configuración
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<AlmacenDbContext>()
                .UseSqlServer(connectionString) // Usar la cadena de conexión real
                .Options;

            _context = new AlmacenDbContext(options);
            _repository = new CategoriaRepository(connectionString, _context);
        }

        [Fact]
        public async Task AddCategoriaAsync_ShouldAddCategoria()
        {
            // Arrange
            var categoria = new Categoria { Nombre = "Test", Descripcion = "Test Descripcion" };
            // Act
            var addedCategoria = await _repository.AddCategoriaAsync(categoria);

            // Assert
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

            // Arrange
            var categorias = new List<Categoria>
            {
                new Categoria { Nombre = "Categoria1", Descripcion = "Descripcion1" },
                new Categoria { Nombre = "Categoria2", Descripcion = "Descripcion2" }
            };

            await _context.Categorias.AddRangeAsync(categorias);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCategoriasAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCategoriaByIdAsync_ShouldReturnCategoria()
        {
            // Arrange
            var categoria = new Categoria { Nombre = "Categoria1", Descripcion = "Descripcion1" };
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCategoriaByIdAsync(categoria.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoria.Nombre, result.Nombre);
            Assert.Equal(categoria.Descripcion, result.Descripcion);
        }

        [Fact]
        public async Task GetCategoriaByIdAsync_ShouldReturnNull_WhenCategoriaDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999;

            // Act
            var result = await _repository.GetCategoriaByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteCategoriaAsync_ShouldRemoveCategoria()
        {
            // Arrange
            var categoria = new Categoria { Nombre = "Categoria1", Descripcion = "Descripcion1" };
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteCategoriaAsync(categoria.Id);

            // Assert
            var deletedCategoria = await _context.Categorias.FindAsync(categoria.Id);
            Assert.Null(deletedCategoria);
        }

        [Fact]
        public async Task DeleteCategoriaAsync_ShouldNotThrow_WhenCategoriaDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999; // ID que no existe

            // Act
            await _repository.DeleteCategoriaAsync(nonExistentId);

            // Assert: no se espera que se produzca una excepción
            Assert.True(true);
        }

        [Fact]
        public async Task UpdateCategoriaAsync_ShouldUpdateCategoria()
        {
            // Arrange
            var categoria = new Categoria { Nombre = "Categoria1", Descripcion = "Descripcion1" };
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            // Act
            categoria.Nombre = "Categoria Actualizada";
            var updatedCategoria = await _repository.UpdateCategoriaAsync(categoria);

            // Assert
            Assert.NotNull(updatedCategoria);
            Assert.Equal("Categoria Actualizada", updatedCategoria.Nombre);
        }

        [Fact]
        public async Task UpdateCategoriaAsync_ShouldThrowException_WhenCategoriaDoesNotExist()
        {
            // Arrange
            var categoria = new Categoria { Id = 999, Nombre = "Categoria No Existente", Descripcion = "Descripcion" };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
                await _repository.UpdateCategoriaAsync(categoria));
        }
    }
}
