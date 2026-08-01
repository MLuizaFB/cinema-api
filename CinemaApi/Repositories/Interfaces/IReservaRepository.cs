namespace CinemaApi.Repositories.Interfaces;

using CinemaApi.Entities;

public interface IReservaRepository
{
    Task AdicionarReservaAsync(Reserva reserva);
    Task<bool> AssentoEstaReservadoAsync(int sessaoId, int assentoId);
    Task<Reserva?> BuscarReservaPorIdAsync(int id);
    Task<IEnumerable<Reserva>> BuscarTodasReservasAsync();
    Task DeletarReservaAsync(Reserva reserva);
}