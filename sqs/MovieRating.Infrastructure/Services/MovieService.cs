using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Infrastructure.Services;

/// <summary>
/// Class <c>MovieService</c> is a service implementation for managing movie data using a database via the Entity Framework Core.
/// </summary>
public class MovieService : IMovieService
{
    private readonly MovieContext _movieContext;

    /// <summary>
    /// Method <c>MovieService</c> initializes a new instance of the MovieService class with the provided MovieContext.
    /// </summary>
    /// <param name="movieContext">The DbContext used to access movie data.</param>
    public MovieService(MovieContext movieContext)
    {
        _movieContext = movieContext;
    }

    /// <summary>
    /// Method <c>ListAllMovies</c> retrieves a list of all movies ordered by their titles.
    /// </summary>
    /// <returns>Returns asynchronously a list of Movie objects.</returns>
    public async Task<List<Movie>> ListAllMovies()
    {
        return await _movieContext.Movies
            .OrderBy(movie => movie.Title)
            .ToListAsync();
    }

    /// <summary>
    /// Method <c>GetMovieByTitle</c> retrieves a movie by its title.
    /// </summary>
    /// <param name="title">The title of the movie to retrieve.</param>
    /// <returns>Returns asynchronously the Movie object with the specified title.</returns>
    public async Task<Movie> GetMovieByTitle(string title)
    {
        return await _movieContext.Movies
            .Include(movie => movie.Ratings)
            .FirstAsync(movie => movie.Title == title);
    }

    /// <summary>
    /// Method <c>GetMovieByTitle</c> checks if a movie with the specified title exists in the database.
    /// </summary>
    /// <param name="title">The title of the movie.</param>
    /// <returns>Returns asynchronously true if the movie exists in the database; otherwise false.</returns>
    public async Task<bool> DoesMovieExist(string title)
    {
        return await _movieContext.Movies
            .AnyAsync(movie => movie.Title == title);
    }

    /// <summary>
    /// Method <c>AddMovie</c> adds a new movie to the database.
    /// </summary>
    /// <param name="movie">The Movie object to add.</param>
    public async Task AddMovie(Movie movie)
    {
        _movieContext.Movies.Add(movie);
        await _movieContext.SaveChangesAsync();
    }
}