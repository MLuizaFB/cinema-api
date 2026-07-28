namespace CinemaApi.Entities
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
        public int Duracao { get; set; }

        public ICollection<Sessao> Sessoes { get; set; } = new List<Sessao>();
    }
}