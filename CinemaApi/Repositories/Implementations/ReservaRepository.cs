namespace CinemaApi.Repositories.Implementations;

using CinemaApi.Data;
using CinemaApi.Entities;
using CinemaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ReservaRepository : IReservaRepository
{
    private readonly CinemaContext _context;

    public ReservaRepository(CinemaContext context)
    {
        _context = context;
    }

    public async Task AdicionarReservaAsync(Reserva reserva)
    {
        await _context.Reservas.AddAsync(reserva);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AssentoEstaReservadoAsync(int sessaoId, int assentoId)
    {
        return await _context.Reservas
            .AnyAsync(r => r.SessaoId == sessaoId && r.AssentoId == assentoId);
    }

    public async Task<Reserva?> BuscarReservaPorIdAsync(int id)
    {
        return await _context.Reservas
            .Include(r => r.Sessao)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Reserva>> BuscarTodasReservasAsync()
    {
        return await _context.Reservas
            .Include(r => r.Sessao)
            .ToListAsync();
    }

    public async Task DeletarReservaAsync(Reserva reserva)
    {
        _context.Reservas.Remove(reserva);
        await _context.SaveChangesAsync();
    }
}