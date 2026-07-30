using CinemaApi.Data;
using CinemaApi.Repositories.Implementations;
using CinemaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CinemaApi.Services.Interfaces;
using CinemaApi.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

//Configuração do banco de dados SQLite
builder.Services.AddDbContext<CinemaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registros
builder.Services.AddScoped<ISessaoRepository, SessaoRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

builder.Services.AddScoped<ISessaoService, SessaoService>();
builder.Services.AddScoped<IReservaService, ReservaService>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CinemaContext>();
    DbInitializer.Initialize(context);
}

app.Run();
