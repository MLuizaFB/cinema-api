namespace CinemaApi.Repositories.Interfaces;

using CinemaApi.Entities;

public interface ISessaoRepository
{
    Task<IEnumerable<Sessao>> BuscarTodasSessoesAsync();
    Task<Sessao?> BuscarSessaoPorIdAsync(int id);
    Task AdicionarSessaoAsync(Sessao sessao);
    Task AtualizarSessaoAsync(Sessao sessao);
    Task DeletarSessaoAsync(Sessao sessao);
}