namespace CinemaApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using CinemaApi.DTOs.Requests;
using CinemaApi.DTOs.Responses;
using CinemaApi.Entities;
using CinemaApi.Services.Interfaces;

[ApiController]
[Route("api/[controller]")] 
public class SessoesController : ControllerBase
{
    private readonly ISessaoService _sessaoService;

    public SessoesController(ISessaoService sessaoService)
    {
        _sessaoService = sessaoService;
    }

    [HttpGet]
    public async Task<IActionResult> BuscarTodas()
    {
        var sessoes = await _sessaoService.BuscarTodasSessoesAsync();
        
        var response = sessoes.Select(s => new SessaoResponseDTO
        {
            Id = s.Id,
            FilmeId = s.FilmeId,
            FilmeTitulo = s.Filme?.Titulo ?? string.Empty, 
            SalaId = s.SalaId,
            SalaNome = s.Sala?.Nome ?? string.Empty,
            DataHora = s.DataHora,
            Preco = s.Preco
        });

        return Ok(response); 
    }

    [HttpGet("{id}/assentos")]
    public async Task<IActionResult> ObterOcupacaoAssentos(int id)
    {
        try
        {
            var assentos = await _sessaoService.ObterOcupacaoAssentosAsync(id);
            return Ok(assentos);
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPost] 
    public async Task<IActionResult> Adicionar([FromBody] SessaoRequestDTO request)
    {
        try
        {
            var novaSessao = new Sessao
            {
                FilmeId = request.FilmeId,
                SalaId = request.SalaId,
                DataHora = request.DataHora,
                Preco = request.Preco
            };

            var sessaoCriada = await _sessaoService.AdicionarSessaoAsync(novaSessao);

            var sessaoCompleta = await _sessaoService.BuscarSessaoPorIdAsync(sessaoCriada.Id);

            var response = new SessaoResponseDTO
            {
                Id = sessaoCompleta!.Id,
                FilmeId = sessaoCompleta.FilmeId,
                FilmeTitulo = sessaoCompleta.Filme?.Titulo ?? string.Empty,
                SalaId = sessaoCompleta.SalaId,
                SalaNome = sessaoCompleta.Sala?.Nome ?? string.Empty,
                DataHora = sessaoCompleta.DataHora,
                Preco = sessaoCompleta.Preco
            };

            return StatusCode(201, response);
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
            await _sessaoService.DeletarSessaoAsync(id);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}