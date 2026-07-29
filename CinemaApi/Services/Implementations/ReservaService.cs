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

        bool assentoOcupado = await _reservaRepository.AssentoEstaReservadoAsync(reserva.SessaoId, reserva.AssentoId);
        if (assentoOcupado)
        {
            throw new Exception("Este assento já está reservado para esta sessão. Por favor escolha outro.");
        }

        await _reservaRepository.AdicionarReservaAsync(reserva);
        
        return reserva;
    }
}