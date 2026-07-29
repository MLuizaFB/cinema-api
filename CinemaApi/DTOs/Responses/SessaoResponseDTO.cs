namespace CinemaApi.DTOs.Responses;

public class SessaoResponseDTO
{
    public int Id { get; set; }
    public int FilmeId { get; set; }
    public string FilmeTitulo { get; set; } = string.Empty;
    public int SalaId { get; set; }
    public string SalaNome { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
    public decimal Preco { get; set; }
}