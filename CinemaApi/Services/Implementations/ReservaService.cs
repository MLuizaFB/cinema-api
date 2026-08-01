namespace CinemaApi.Services.Implementations;

using CinemaApi.Entities;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services.Interfaces;

public class ReservaService : IReservaService
{
    private readonly IReservaRepository _reservaRepository;
    private readonly ISessaoRepository _sessaoRepository;

    public ReservaService(IReservaRepository reservaRepository, ISessaoRepository sessaoRepository)
    {
        _reservaRepository = reservaRepository;
        _sessaoRepository = sessaoRepository;
    }

    public async Task<Reserva> AdicionarReservaAsync(Reserva reserva)
    {
        var sessao = await _sessaoRepository.BuscarSessaoPorIdAsync(reserva.SessaoId);
        if (sessao == null)
        {
            throw new Exception("A sessão informada não existe.");
        }

        if (sessao.DataHora < DateTime.Now)
        {
            throw new Exception("Não é possível reservar assentos para uma sessão que já encerrou ou já começou.");
        }

        bool assentoValido = await _sessaoRepository.AssentoPertenceASalaAsync(sessao.SalaId, reserva.AssentoId);
        if (!assentoValido)
        {
            throw new Exception("O assento escolhido não existe ou não pertence à sala desta sessão.");
        }

        bool assentoOcupado = await _reservaRepository.AssentoEstaReservadoAsync(reserva.SessaoId, reserva.AssentoId);
        if (assentoOcupado)
        {
            throw new Exception("Este assento já está reservado para esta sessão. Por favor escolha outro.");
        }

        await _reservaRepository.AdicionarReservaAsync(reserva);
        
        return reserva;
    }

    public async Task<IEnumerable<Reserva>> BuscarTodasReservasAsync()
    {
        return await _reservaRepository.BuscarTodasReservasAsync();
    }

    public async Task DeletarReservaAsync(int id)
    {
        var reserva = await _reservaRepository.BuscarReservaPorIdAsync(id);
        
        if (reserva == null)
        {
            throw new Exception("Reserva não encontrada.");
        }

        if (reserva.Sessao != null && reserva.Sessao.DataHora < DateTime.Now)
        {
            throw new Exception("Não é possível cancelar uma reserva de uma sessão que já iniciou ou encerrou.");
        }

        await _reservaRepository.DeletarReservaAsync(reserva);
    }
}