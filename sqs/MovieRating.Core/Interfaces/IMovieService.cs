using MovieRating.Core.Models;

namespace MovieRating.Core.Interfaces;

/// <summary>
/// Interface <c>IMovieService</c> provides methods to manage movie models
/// </summary>
public interface IMovieService
{
    /// <summary>
    /// Method <c>ListAllMovies</c> lists all movies which are saved in the database.
    /// </summary>
    /// <returns> Returns asynchronously a list of <c>Movie</c> objects.</returns>
    Task<List<Movie>> ListAllMovies();

    /// <summary>
    /// Method <c>GetMovieByTitle</c> retrieves a movie by its title.
    /// </summary>
    /// <param name="title">The title of the desired movie.</param>
    /// <returns> Returns asynchronously the <c>Movie</c> object with the movie information.</returns>
    Task<Movie> GetMovieByTitle(string title);

    /// <summary>
    /// Method <c>DoesMovieExist</c> checks if a movie exists in the database, based on its title.
    /// </summary>
    /// <param name="title">The title of the desired movie.</param>
    /// <returns>Returns asynchronously a boolean value indicating whether the movie exists.</returns>
    Task<bool> DoesMovieExist(string title);

    /// <summary>
    /// Method <c>AddMovie</c> adds a new movie to the database.
    /// </summary>
    /// <param name="movie">The <c>Movie</c> object to be added.</param>
    Task AddMovie(Movie movie);
}