namespace CinemaApi.Entities;

public class Reserva
{
    public int Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;
    public int SessaoId { get; set; }
    public Sessao? Sessao { get; set; }
    public int AssentoId { get; set; }
    public Assento? Assento { get; set; }
}