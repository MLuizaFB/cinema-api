namespace CinemaApi.Services.Implementations;

using CinemaApi.Entities;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services.Interfaces;

public class SessaoService : ISessaoService
{
    private readonly ISessaoRepository _sessaoRepository;

    public SessaoService(ISessaoRepository sessaoRepository)
    {
        _sessaoRepository = sessaoRepository;
    }

    public async Task<IEnumerable<Sessao>> BuscarTodasSessoesAsync()
    {
        return await _sessaoRepository.BuscarTodasSessoesAsync();
    }

    public async Task<Sessao?> BuscarSessaoPorIdAsync(int id)
    {
        var sessao = await _sessaoRepository.BuscarSessaoPorIdAsync(id);
        
        if (sessao == null)
        {
            throw new Exception("Sessão não encontrada.");
        }
        
        return sessao;
    }

    public async Task<Sessao> AdicionarSessaoAsync(Sessao sessao)
    {
        ValidarDadosSessao(sessao);

        bool temConflito = await _sessaoRepository.ExisteSessaoConflitanteAsync(sessao.SalaId, sessao.DataHora, sessao.FilmeId);
        if (temConflito)
        {
            throw new Exception("Conflito de horário: Já existe uma sessão ocupando a sala neste período");
        }

        await _sessaoRepository.AdicionarSessaoAsync(sessao);
        return sessao;
    }

    public async Task AtualizarSessaoAsync(int id, Sessao sessaoAtualizada)
    {
        var sessaoExistente = await _sessaoRepository.BuscarSessaoPorIdAsync(id);
        
        if (sessaoExistente == null)
        {
            throw new Exception("Sessão não encontrada para atualização.");
        }

        ValidarDadosSessao(sessaoAtualizada);

        bool temConflito = await _sessaoRepository.ExisteSessaoConflitanteAsync(sessaoAtualizada.SalaId, sessaoAtualizada.DataHora, sessaoAtualizada.FilmeId, id);
        if (temConflito)
        {
            throw new Exception("Conflito de horário: Já existe uma sessão ocupando a sala neste período.");
        }

        sessaoExistente.DataHora = sessaoAtualizada.DataHora;
        sessaoExistente.Preco = sessaoAtualizada.Preco;
        sessaoExistente.FilmeId = sessaoAtualizada.FilmeId;
        sessaoExistente.SalaId = sessaoAtualizada.SalaId;

        await _sessaoRepository.AtualizarSessaoAsync(sessaoExistente);
    }

    public async Task DeletarSessaoAsync(int id)
    {
        var sessao = await _sessaoRepository.BuscarSessaoPorIdAsync(id);
        
        if (sessao == null)
        {
            throw new Exception("Sessão não encontrada para exclusão.");
        }

        await _sessaoRepository.DeletarSessaoAsync(sessao);
    }

    private void ValidarDadosSessao(Sessao sessao)
    {
        if (sessao.Preco <= 0)
        {
            throw new Exception("O preço da sessão deve ser maior que zero.");
        }

        if (sessao.DataHora < DateTime.Now)
        {
            throw new Exception("O horário da sessão não pode estar no passado.");
        }
    }
}