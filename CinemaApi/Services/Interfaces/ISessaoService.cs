namespace CinemaApi.Services.Interfaces;

using CinemaApi.Entities;
using CinemaApi.DTOs.Responses;

public interface ISessaoService
{
    Task<IEnumerable<Sessao>> BuscarTodasSessoesAsync();
    Task<Sessao?> BuscarSessaoPorIdAsync(int id);
    Task<Sessao> AdicionarSessaoAsync(Sessao sessao);
    Task AtualizarSessaoAsync(int id, Sessao sessaoAtualizada);
    Task DeletarSessaoAsync(int id);
    Task<IEnumerable<AssentoOcupacaoResponseDTO>> ObterOcupacaoAssentosAsync(int sessaoId);
}