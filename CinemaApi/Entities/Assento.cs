namespace CinemaApi.Entities;

public class Assento
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public int SalaId { get; set; }
    public Sala? Sala { get; set; }
}