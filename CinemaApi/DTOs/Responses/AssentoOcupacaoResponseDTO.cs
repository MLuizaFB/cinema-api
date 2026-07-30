namespace CinemaApi.DTOs.Responses;

public class AssentoOcupacaoResponseDTO
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public bool Ocupado { get; set; }
}