using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Requests;

public class SessaoRequestDTO
{
    [Required]
    public int FilmeId { get; set; }
    
    [Required]
    public int SalaId { get; set; }
    
    [Required]
    public DateTime DataHora { get; set; }

    [Required]
    public decimal Preco { get; set; }
}