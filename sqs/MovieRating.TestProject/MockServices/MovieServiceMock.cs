using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.TestProject.MockServices;

/// <summary>
/// Class <c>MovieServiceMock</c> is a mock implementation of the <c>IMovieService</c> interface for testing purposes.
/// </summary>
public class MovieServiceMock : IMovieService
{
    private readonly List<Movie> _movieList = new();

    /// <summary>
    /// Method <c>ListAllMovies</c> returns a list of all movies.
    /// </summary>
    /// <returns>Returns asynchronously a list of <c>Movie</c> objects.</returns>
    public Task<List<Movie>> ListAllMovies()
    {
        return Task.FromResult(_movieList);
    }

    /// <summary>
    /// Method <c>GetMovieByTitle</c> retrieves a movie based on its title.
    /// </summary>
    /// <param name="title">The title of the movie to search for.</param>
    /// <returns>Returns asynchronously a <c>Movie</c> object.</returns>
    /// <exception cref="NoSuchMovieException">Thrown when the specified movie title is not found in the list.</exception>
    public Task<Movie> GetMovieByTitle(string title)
    {
        foreach (var movie in _movieList)
        {
            if (movie.Title == title) return Task.FromResult(movie);
        }

        throw new NoSuchMovieException("Movie is not in DB");
    }

    /// <summary>
    /// Method <c>DoesMovieExist</c> checks if a movie with the specified title exists.
    /// </summary>
    /// <param name="title">The title of the movie to check for.</param>
    /// <returns>Returns asynchronously a boolean indicating whether the movie exists.</returns>
    public Task<bool> DoesMovieExist(string title)
    {
        foreach (var movie in _movieList)
        {
            if (movie.Title == title) return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    /// <summary>
    /// Method <c>AddMovie</c> adds a new movie to the list.
    /// </summary>
    /// <param name="movie">The <c>Movie</c> object to add.</param>
    public Task AddMovie(Movie movie)
    {
        _movieList.Add(movie);
        return Task.CompletedTask;
    }
}