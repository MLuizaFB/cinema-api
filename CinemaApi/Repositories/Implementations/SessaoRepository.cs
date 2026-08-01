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

    public async Task<bool> AssentoPertenceASalaAsync(int salaId, int assentoId)
    {
        return await _context.Set<Assento>()
            .AnyAsync(a => a.Id == assentoId && a.SalaId == salaId);
    }

    public async Task<bool> ExisteSessaoConflitanteAsync(int salaId, DateTime horarioNovaSessao, int filmeId, int? sessaoIdIgnorada = null)
    {
        var filmeNovo = await _context.Set<Filme>().FindAsync(filmeId);
        if (filmeNovo == null) return false; 

        var fimNovaSessao = horarioNovaSessao.AddMinutes(filmeNovo.Duracao);

        DateTime inicioDoDia = horarioNovaSessao.Date;
        DateTime fimDoDia = inicioDoDia.AddDays(1);

        var query = _context.Sessoes
            .Include(s => s.Filme) 
            .Where(s => s.SalaId == salaId && s.DataHora >= inicioDoDia && s.DataHora < fimDoDia);

        if (sessaoIdIgnorada.HasValue)
        {
            query = query.Where(s => s.Id != sessaoIdIgnorada.Value);
        }

        var sessoesNoMesmoDia = await query.ToListAsync();

        return sessoesNoMesmoDia.Any(sessaoExistente => 
        {
            var fimSessaoExistente = sessaoExistente.DataHora.AddMinutes(sessaoExistente.Filme.Duracao);
            
            return horarioNovaSessao < fimSessaoExistente && fimNovaSessao > sessaoExistente.DataHora;
        });
    }

    public async Task<IEnumerable<Assento>> BuscarAssentosDaSalaAsync(int salaId)
    {
        return await _context.Set<Assento>()
            .Where(a => a.SalaId == salaId)
            .OrderBy(a => a.Codigo)
            .ToListAsync();
    }

    public async Task<IEnumerable<int>> BuscarIdsAssentosReservadosAsync(int sessaoId)
    {
        return await _context.Set<Reserva>()
            .Where(r => r.SessaoId == sessaoId)
            .Select(r => r.AssentoId)
            .ToListAsync();
    }

    public async Task<bool> FilmeExisteAsync(int filmeId)
    {
        return await _context.Filmes.AnyAsync(f => f.Id == filmeId);
    }

    public async Task<bool> SalaExisteAsync(int salaId)
    {
        return await _context.Salas.AnyAsync(s => s.Id == salaId);
    }
}