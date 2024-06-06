using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Web.Services;

public class InfoService : IInfoService
{
    private string _apiKey;

    public InfoService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<Movie?> GetMovieInfo(string title)
    {
        var client = new HttpClient();
        var response = await client.GetStringAsync($"http://www.omdbapi.com/?apikey={_apiKey}&t={title}");

        Console.WriteLine(response);

        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = null,
            Director = null,
            Duration = 0,
            Genre = null,
            Description = null
        };
    }
}