using FilmesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private static List<Filme> filmes = new List<Filme>();

    [HttpPost]
    public void AdicionarFilme([FromBody] Filme filme)
    {
        filmes.Add(filme);

        Console.WriteLine($"Filme '{filme.Titulo}' adicionado com sucesso!");
        Console.WriteLine($"Duração: {filme.Duracao} minutos");
    }
}
