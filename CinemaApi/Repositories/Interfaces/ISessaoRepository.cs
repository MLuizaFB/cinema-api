namespace CinemaApi.Repositories.Interfaces;

using CinemaApi.Entities;

public interface ISessaoRepository
{
    Task<IEnumerable<Sessao>> BuscarTodasSessoesAsync();
    Task<Sessao?> BuscarSessaoPorIdAsync(int id);
    Task AdicionarSessaoAsync(Sessao sessao);
    Task AtualizarSessaoAsync(Sessao sessao);
    Task DeletarSessaoAsync(Sessao sessao);
    Task<bool> AssentoPertenceASalaAsync(int salaId, int assentoId);
    Task<bool> ExisteSessaoConflitanteAsync(int salaId, DateTime dataHora, int filmeId, int? sessaoIdIgnorada = null);
}