using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Requests;

public class ReservaRequestDTO
{
    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    public string NomeCliente { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail do cliente é obrigatório.")]
    [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
    public string EmailCliente { get; set; } = string.Empty;

    [Required]
    public int SessaoId { get; set; }

    [Required]
    public int AssentoId { get; set; }
}