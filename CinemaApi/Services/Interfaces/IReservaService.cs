namespace CinemaApi.Services.Interfaces;

using CinemaApi.Entities;

public interface IReservaService
{
    Task<Reserva> AdicionarReservaAsync(Reserva reserva);
}