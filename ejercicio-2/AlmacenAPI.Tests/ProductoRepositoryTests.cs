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
    public class ProductoRepositoryTests
    {
        private readonly AlmacenDbContext _context;
        private readonly ProductoRepository _repository;

        public ProductoRepositoryTests()
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
            _repository = new ProductoRepository(connectionString, _context);
        }

        private async Task SeedCategoriesAsync()
        {
            // Asegúrate de que las categorías no se dupliquen
            if (!_context.Categorias.Any())
            {
                await _context.Categorias.AddRangeAsync(new List<Categoria>
                {
                    new Categoria { Id = 50, Nombre = "Categoria1", Descripcion = "Descripcion1" },
                    new Categoria { Id = 51, Nombre = "Categoria2", Descripcion = "Descripcion2" }
                });

                await _context.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetProductosAsync_ShouldReturnAllProductos()
        {
            // Arrange
            var categoriaId = 2; // Asegúrate de que este ID exista en Categorias
            var producto = new Producto { Nombre = "ProductoTest", Descripcion = "DescripcionTest", CategoriaId = categoriaId };

            // Agregar producto a la base de datos
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProductosAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, p => p.Nombre == "ProductoTest");
        }


        [Fact]
        public async Task GetProductosByCategoriaAsync_ShouldReturnProductosInCategoria()
        {
            
            var categoriaId = 2; 
            
            var categoria = await _context.Categorias.FindAsync(categoriaId);
            Assert.NotNull(categoria); 

            
            var result = await _repository.GetProductosByCategoriaAsync(categoriaId);

           
            Assert.NotNull(result);
            Assert.All(result, p => Assert.Equal(categoriaId, p.CategoriaId)); 
        }


        [Fact]
        public async Task GetProductoByIdAsync_ShouldReturnProducto()
        {
            // Arrange
            var categoriaId = 2; // Asegúrate de que este ID exista en Categorias
            var producto = new Producto { Nombre = "ProductoTest", Descripcion = "DescripcionTest", CategoriaId = categoriaId };

            // Agregar producto a la base de datos
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProductoByIdAsync(producto.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(producto.Nombre, result.Nombre);
        }


        [Fact]
        public async Task GetProductoByIdAsync_ShouldReturnNull_WhenProductoDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999;

            // Act
            var result = await _repository.GetProductoByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddProductoAsync_ShouldAddProducto()
        {
            // Arrange
            var categoriaId = 2; // Asegúrate de que este ID existe en la base de datos
            var categoria = new Categoria { Id = categoriaId, Nombre = "CategoriaTest", Descripcion = "DescripcionTest" };

            // Agregar la categoría solo si no existe en la base de datos de prueba
            var existingCategoria = await _context.Categorias.FindAsync(categoriaId);
            if (existingCategoria == null)
            {
                await _context.Categorias.AddAsync(categoria);
                await _context.SaveChangesAsync();
            }

            // Crear un nuevo producto que pertenezca a la categoría existente
            var producto = new Producto { Nombre = "ProductoTest", Descripcion = "DescripcionTest", CategoriaId = categoriaId };

            // Act
            await _repository.AddProductoAsync(producto);
            await _context.SaveChangesAsync(); // Asegúrate de guardar los cambios

            // Assert
            var result = await _context.Productos.FindAsync(producto.Id);
            Assert.NotNull(result);
            Assert.Equal(producto.Nombre, result.Nombre);
            Assert.Equal(categoriaId, result.CategoriaId);
        }


        [Fact]
        public async Task UpdateProductoAsync_ShouldUpdateProducto()
        {
            // Arrange
            var categoriaId = 2; // Asegúrate de que este ID exista en la tabla Categorias
            var producto = new Producto { Nombre = "Producto1", Descripcion = "Descripcion1", CategoriaId = categoriaId };

            // Agregar producto a la base de datos
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            // Act
            producto.Nombre = "Producto Actualizado"; // Cambiar el nombre del producto
            var updatedProducto = await _repository.UpdateProductoAsync(producto);

            // Assert
            Assert.NotNull(updatedProducto);
            Assert.Equal("Producto Actualizado", updatedProducto.Nombre);
        }


        [Fact]
        public async Task UpdateProductoAsync_ShouldThrowException_WhenProductoDoesNotExist()
        {
            // Arrange
            var producto = new Producto { Id = 999, Nombre = "Producto No Existente", Descripcion = "Descripcion" };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
                await _repository.UpdateProductoAsync(producto));
        }

        [Fact]
        public async Task DeleteProductoAsync_ShouldRemoveProducto()
        {
            // Arrange
            var categoria = new Categoria { Nombre = "CategoriaTest", Descripcion = "DescripcionTest" };

            // Agregar la categoría solo si no existe en la base de datos de prueba
            var existingCategoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Nombre == categoria.Nombre); // Busca por nombre o cualquier otra propiedad única
            if (existingCategoria == null)
            {
                await _context.Categorias.AddAsync(categoria);
                await _context.SaveChangesAsync();
            }
            else
            {
                categoria = existingCategoria; // Usa la categoría existente
            }

            // Agregar un producto que pertenece a la categoría
            var producto = new Producto { Nombre = "ProductoTest", Descripcion = "DescripcionTest", CategoriaId = categoria.Id };
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteProductoAsync(producto.Id);
            await _context.SaveChangesAsync(); // Asegúrate de guardar los cambios

            // Assert
            var result = await _context.Productos.FindAsync(producto.Id);
            Assert.Null(result); // Asegúrate de que el producto ya no existe
        }



        [Fact]
        public async Task DeleteProductoAsync_ShouldNotThrow_WhenProductoDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999;

            // Act
            await _repository.DeleteProductoAsync(nonExistentId);

            // Assert: no se espera que se produzca una excepción
            Assert.True(true);
        }
    }
}
