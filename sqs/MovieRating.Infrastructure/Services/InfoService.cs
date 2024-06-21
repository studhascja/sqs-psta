using System.Net.Http.Json;
using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Infrastructure.Services;

/// <summary>
/// Class <c>InfoService</c> is a Service implementation for retrieving movie information from an external API.
/// </summary>
public class InfoService : IInfoService
{
    private readonly string? _apiKey;

    /// <summary>
    /// Method <c>InfoService</c> initializes a new instance of the <c>InfoService</c> with an API key.
    /// </summary>
    /// <param name="apiKey">The API key used to access the external movie database.</param>
    public InfoService(string apiKey)
    {
        _apiKey = apiKey;
    }

    /// <summary>
    /// Method <c>GetMovieInfo</c> retrieves detailed information, from the API, about a movie based on its title.
    /// </summary>
    /// <param name="title">The title of the movie to search for.</param>
    /// <returns>Returns a <c>Movie</c> object.</returns>
    /// <exception cref="NoSuchMovieException">Thrown when the Movie with the specified movie title does not exist in the API.</exception>
    public async Task<Movie> GetMovieInfo(string title)
    {
        using var client = new HttpClient();
        try
        {
            // Make a request to the external API to fetch movie information
            var response =
                await client.GetFromJsonAsync<MovieDto>($"https://www.omdbapi.com/?apikey={_apiKey}&t={title}");
            // Convert the MovieDto received from the API to a Movie object
            return ChangeToMovieDto(response!);
        }
        catch (Exception)
        {
            throw new NoSuchMovieException("Movie " + title + "does not exist.");
        }
    }

    /// <summary>
    /// Method <c>ChangeToMovieDto</c> converts a MovieDto object to a Movie object.
    /// </summary>
    /// <param name="movieDto">The MovieDto object to convert.</param>
    /// <returns>A new instance of <see cref="Movie"/> based on the provided MovieDto.</returns>
    public Movie ChangeToMovieDto(MovieDto movieDto)
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = movieDto.Title,
            Director = movieDto.Director,
            Duration = movieDto.Runtime,
            Genre = movieDto.Genre,
            Description = movieDto.Plot,
            Ratings = []
        };
    }
}