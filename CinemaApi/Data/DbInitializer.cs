namespace CinemaApi.Data;

using CinemaApi.Entities;

public static class DbInitializer
{
    public static void Initialize(CinemaContext context)
    {
        context.Database.EnsureCreated();

        if (context.Filmes.Any())
        {
            return;
        }

        var filmes = new[]
        {
            new Filme 
            { 
                Titulo = "Piratas do Caribe: A Maldição do Pérola Negra", 
                Sinopse = "O ferreiro Will Turner se une ao excêntrico pirata Jack Sparrow para resgatar o amor de sua vida da tripulação amaldiçoada do Pérola Negra.", 
                Duracao = 143 
            },
            new Filme 
            { 
                Titulo = "Piratas do Caribe: O Baú da Morte", 
                Sinopse = "Jack Sparrow busca recuperar a chave do Baú da Morte para saldar sua dívida de sangue com o temível Davy Jones.", 
                Duracao = 151 
            },
            new Filme 
            { 
                Titulo = "Piratas do Caribe: No Fim do Mundo", 
                Sinopse = "Will, Elizabeth e Barbossa precisam navegar até os confins da Terra para resgatar Jack Sparrow do Baú de Davy Jones.", 
                Duracao = 169 
            }
        };
        context.Filmes.AddRange(filmes);
        context.SaveChanges();

        var salas = new[]
        {
            new Sala { Nome = "Sala 1", Capacidade = 10 },
            new Sala { Nome = "Sala 2", Capacidade = 10 }
        };
        context.Salas.AddRange(salas);
        context.SaveChanges();

        var assentos = new List<Assento>();
        foreach (var sala in salas)
        {
            for (int i = 1; i <= 5; i++)
            {
                assentos.Add(new Assento { Codigo = $"A{i}", SalaId = sala.Id });
                assentos.Add(new Assento { Codigo = $"B{i}", SalaId = sala.Id });
            }
        }
        context.Assentos.AddRange(assentos);
        context.SaveChanges();
    }
}