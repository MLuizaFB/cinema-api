namespace CinemaApi.DTOs.Responses;

public class ReservaResponseDTO
{
    public int Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;
    public int SessaoId { get; set; }
    public int AssentoId { get; set; }
}