using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Web.Services;

public class InfoService : IInfoService
{
    private readonly string? _apiKey;
    private static readonly string EnvironmentNameApiKey = "API_KEY";
    private readonly string? _environmentValueApiKey = Environment.GetEnvironmentVariable(EnvironmentNameApiKey);
    public InfoService()
    {
        _apiKey = _environmentValueApiKey ?? throw new EnvironmentVariableNotSetException("API-Key not set correctly");
    }

    public async Task<Movie?> GetMovieInfo(string title)
    {
        using var client = new HttpClient();
        var response = await client.GetFromJsonAsync<MovieDto>($"http://www.omdbapi.com/?apikey={_apiKey}&t={title}");

        return ChangeToMovieDto(response ?? throw new NoSuchMovieException("Movie " + title + "does not exist."));
    }

    public Movie ChangeToMovieDto(MovieDto movieDto)
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = movieDto.Title,
            Director = movieDto.Director,
            Duration = movieDto.Runtime,
            Genre = movieDto.Genre,
            Description = movieDto.Plot
        };
    }
}