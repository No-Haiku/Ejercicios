using AlmacenAPI.Infrastructure;
using AlmacenAPI.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework Core para otras entidades que lo necesiten
builder.Services.AddDbContext<AlmacenDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar MediatR para manejar comandos y queries
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Registrar los repositorios con Dapper
builder.Services.AddScoped<IProductoRepository>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var context = provider.GetRequiredService<AlmacenDbContext>();
    return new ProductoRepository(connectionString, context);
});

builder.Services.AddScoped<ICategoriaRepository>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var context = provider.GetRequiredService<AlmacenDbContext>();
    return new CategoriaRepository(connectionString, context);
});


// Otros servicios necesarios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuraciones de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configuración de Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Autorizar peticiones
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();


app.Run();
