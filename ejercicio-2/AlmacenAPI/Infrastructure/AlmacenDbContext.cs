using Microsoft.EntityFrameworkCore;
using AlmacenAPI.Domain;

namespace AlmacenAPI.Infrastructure
{
    public class AlmacenDbContext : DbContext
    {
        public AlmacenDbContext(DbContextOptions<AlmacenDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId);
        }
    }
}
