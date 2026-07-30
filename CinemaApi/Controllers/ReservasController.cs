namespace CinemaApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using CinemaApi.DTOs.Requests;
using CinemaApi.DTOs.Responses;
using CinemaApi.Entities;
using CinemaApi.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservasController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ReservaRequestDTO request)
    {
        try
        {
            var novaReserva = new Reserva
            {
                NomeCliente = request.NomeCliente,
                EmailCliente = request.EmailCliente,
                SessaoId = request.SessaoId,
                AssentoId = request.AssentoId
            };

            var reservaCriada = await _reservaService.AdicionarReservaAsync(novaReserva);

            var response = new ReservaResponseDTO
            {
                Id = reservaCriada.Id,
                NomeCliente = reservaCriada.NomeCliente,
                EmailCliente = reservaCriada.EmailCliente,
                SessaoId = reservaCriada.SessaoId,
                AssentoId = reservaCriada.AssentoId
            };

            return StatusCode(201, new { mensagem = "Reserva realizada com sucesso!", reserva = response });
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        try
        {
            await _reservaService.DeletarReservaAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}