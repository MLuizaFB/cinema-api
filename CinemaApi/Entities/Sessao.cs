namespace CinemaApi.Entities;

public class Sessao
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public decimal Preco { get; set; }
    public int FilmeId { get; set; }
    public Filme? Filme { get; set; }
    public int SalaId { get; set; }
    public Sala? Sala { get; set; }
    
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}