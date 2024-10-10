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
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json") 
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<AlmacenDbContext>()
                .UseSqlServer(connectionString) 
                .Options;

            _context = new AlmacenDbContext(options);
            _repository = new ProductoRepository(connectionString, _context);
        }

        private async Task SeedCategoriesAsync()
        {
            
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
            
            var categoriaId = 2; 
            var producto = new Producto { Nombre = "ProductoTest", Descripcion = "DescripcionTest", CategoriaId = categoriaId };

            
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            
            var result = await _repository.GetProductosAsync();

            
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
            
            var categoriaId = 2; 
            var producto = new Producto { Nombre = "ProductoTest", Descripcion = "DescripcionTest", CategoriaId = categoriaId };

            
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            
            var result = await _repository.GetProductoByIdAsync(producto.Id);

            
            Assert.NotNull(result);
            Assert.Equal(producto.Nombre, result.Nombre);
        }


        [Fact]
        public async Task GetProductoByIdAsync_ShouldReturnNull_WhenProductoDoesNotExist()
        {
            
            int nonExistentId = 999;

            
            var result = await _repository.GetProductoByIdAsync(nonExistentId);

            
            Assert.Null(result);
        }

        [Fact]
        public async Task AddProductoAsync_ShouldAddProducto()
        {
            
            var categoriaId = 2; 
            var categoria = new Categoria { Id = categoriaId, Nombre = "CategoriaTest", Descripcion = "DescripcionTest" };

            
            var existingCategoria = await _context.Categorias.FindAsync(categoriaId);
            if (existingCategoria == null)
            {
                await _context.Categorias.AddAsync(categoria);
                await _context.SaveChangesAsync();
            }

            
            var producto = new Producto { Nombre = "ProductoTest", Descripcion = "DescripcionTest", CategoriaId = categoriaId };

            
            await _repository.AddProductoAsync(producto);
            await _context.SaveChangesAsync(); 

            
            var result = await _context.Productos.FindAsync(producto.Id);
            Assert.NotNull(result);
            Assert.Equal(producto.Nombre, result.Nombre);
            Assert.Equal(categoriaId, result.CategoriaId);
        }


        [Fact]
        public async Task UpdateProductoAsync_ShouldUpdateProducto()
        {
            
            var categoriaId = 2; 
            var producto = new Producto { Nombre = "Producto1", Descripcion = "Descripcion1", CategoriaId = categoriaId };

            
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            
            producto.Nombre = "Producto Actualizado"; 
            var updatedProducto = await _repository.UpdateProductoAsync(producto);

            
            Assert.NotNull(updatedProducto);
            Assert.Equal("Producto Actualizado", updatedProducto.Nombre);
        }


        [Fact]
        public async Task UpdateProductoAsync_ShouldThrowException_WhenProductoDoesNotExist()
        {
            
            var producto = new Producto { Id = 999, Nombre = "Producto No Existente", Descripcion = "Descripcion" };

           
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
                await _repository.UpdateProductoAsync(producto));
        }

        [Fact]
        public async Task DeleteProductoAsync_ShouldRemoveProducto()
        {
            
            var categoria = new Categoria { Nombre = "CategoriaTest", Descripcion = "DescripcionTest" };

            
            var existingCategoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Nombre == categoria.Nombre); // Busca por nombre o cualquier otra propiedad única
            if (existingCategoria == null)
            {
                await _context.Categorias.AddAsync(categoria);
                await _context.SaveChangesAsync();
            }
            else
            {
                categoria = existingCategoria; 
            }

            
            var producto = new Producto { Nombre = "ProductoTest", Descripcion = "DescripcionTest", CategoriaId = categoria.Id };
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            
            await _repository.DeleteProductoAsync(producto.Id);
            await _context.SaveChangesAsync(); 

            
            var result = await _context.Productos.FindAsync(producto.Id);
            Assert.Null(result); 
        }



        [Fact]
        public async Task DeleteProductoAsync_ShouldNotThrow_WhenProductoDoesNotExist()
        {
            
            int nonExistentId = 999;

            
            await _repository.DeleteProductoAsync(nonExistentId);

            
            Assert.True(true);
        }
    }
}
