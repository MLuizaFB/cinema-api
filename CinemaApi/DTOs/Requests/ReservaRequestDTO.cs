namespace CinemaApi.DTOs.Requests;

public class ReservaRequestDTO
{
    public string NomeCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;
    public int SessaoId { get; set; }
    public int AssentoId { get; set; }
}