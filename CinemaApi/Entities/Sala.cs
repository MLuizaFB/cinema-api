namespace CinemaApi.Entities;

public class Sala
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Capacidade { get; set; }

    public ICollection<Assento> Assentos { get; set; } = new List<Assento>();
    public ICollection<Sessao> Sessoes { get; set; } = new List<Sessao>();
}