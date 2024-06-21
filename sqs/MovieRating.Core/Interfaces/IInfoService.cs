using MovieRating.Core.Models;

namespace MovieRating.Core.Interfaces;

/// <summary>
/// Interface <c>IInfoService</c> provides methods to retrieve movie information from API
/// </summary>
public interface IInfoService
{
    /// <summary>
    /// Method <c>GetMovieInfo</c> retrieves information about a movie based on its title.
    /// </summary>
    /// <param name="title">The title of the desired movie.</param>
    /// <returns>Provides asynchronously the <c>Movie</c> object with the movie information.</returns>
    Task<Movie> GetMovieInfo(string title);
}