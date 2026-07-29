namespace CinemaApi.DTOs.Requests;

public class SessaoRequestDTO
{
    public int FilmeId { get; set; }
    public int SalaId { get; set; }
    public DateTime DataHora { get; set; }
    public decimal Preco { get; set; }
}