namespace CinemaApi.Repositories.Interfaces;

using CinemaApi.Entities;

public interface IReservaRepository
{
    Task AdicionarReservaAsync(Reserva reserva);
    Task<bool> AssentoEstaReservadoAsync(int sessaoId, int assentoId);
}