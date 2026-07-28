namespace CinemaApi.Data;

using CinemaApi.Entities;
using Microsoft.EntityFrameworkCore;

public class CinemaContext : DbContext
{
    public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) { }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
    public DbSet<Assento> Assentos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Regra para garantir que uma reserva de assento por sessão seja única
        modelBuilder.Entity<Reserva>()
            .HasIndex(r => new { r.SessaoId, r.AssentoId })
            .IsUnique();
    }
}