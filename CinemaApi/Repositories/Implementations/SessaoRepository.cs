namespace CinemaApi.Repositories.Implementations;

using CinemaApi.Data;
using CinemaApi.Entities;
using CinemaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class SessaoRepository : ISessaoRepository
{
    private readonly CinemaContext _context;

    public SessaoRepository(CinemaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sessao>> BuscarTodasSessoesAsync()
    {
        return await _context.Sessoes
            .Include(s => s.Filme)
            .Include(s => s.Sala)
            .ToListAsync();
    }

    public async Task<Sessao?> BuscarSessaoPorIdAsync(int id)
    {
        return await _context.Sessoes
            .Include(s => s.Filme)
            .Include(s => s.Sala)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AdicionarSessaoAsync(Sessao sessao)
    {
        await _context.Sessoes.AddAsync(sessao);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarSessaoAsync(Sessao sessao)
    {
        _context.Sessoes.Update(sessao);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarSessaoAsync(Sessao sessao)
    {
        _context.Sessoes.Remove(sessao);
        await _context.SaveChangesAsync();
    }
}