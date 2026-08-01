namespace CinemaApi.Services.Interfaces;

using CinemaApi.Entities;

public interface IReservaService
{
    Task<Reserva> AdicionarReservaAsync(Reserva reserva);
    Task<IEnumerable<Reserva>> BuscarTodasReservasAsync();
    Task DeletarReservaAsync(int id);
}