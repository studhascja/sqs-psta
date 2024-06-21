using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Infrastructure.Services;

/// <summary>
/// Class <c>RatingService</c> is a Service implementation for managing movie ratings using the database via Entity Framework Core.
/// </summary>
public class RatingService : IRatingService
{
    private readonly MovieContext _movieContext;

    /// <summary>
    /// Method <c>RatingService</c> initializes a new instance of the RatingService class with the provided MovieContext.
    /// </summary>
    /// <param name="movieContext">The DbContext used to access movie data.</param>
    public RatingService(MovieContext movieContext)
    {
        _movieContext = movieContext;
    }

    /// <summary>
    /// Method <c>ListAllRatings</c> retrieves all ratings for a specific movie by its title.
    /// </summary>
    /// <param name="title">The title of the movie.</param>
    /// <returns>Returns asynchronously a list of Rating objects.</returns>
    public async Task<List<Rating>> ListAllRatings(string title)
    {
        var movieList = await _movieContext.Movies
            .Where(movie => movie.Title == title)
            .Include(movie => movie.Ratings)
            .ToListAsync();

        return GetRatings(movieList, title);
    }

    /// <summary>
    /// Method <c>AddRating</c> adds a new rating to the database.
    /// </summary>
    /// <param name="rating">The Rating object to be added.</param>
    public async Task AddRating(Rating rating)
    {
        _movieContext.Ratings.Add(rating);

        await _movieContext.SaveChangesAsync();
    }

    /// <summary>
    /// Method <c>GetRatings</c> uses a movie title to search for a suitable movie from a list and displays all of its ratings.
    /// </summary>
    /// <param name="movieList">The List of Movies</param>
    /// <param name="title">Title of the searched Movie</param>
    /// <returns>Returns a list of Rating objects.</returns>
    /// <exception cref="ToManyMatchingRatingsException">Thrown when more than one matching movie is found for the specified movie title.</exception>
    /// <exception cref="NoMatchingRatingException">Thrown when no matching movie is found for the specified movie title.</exception>
    private List<Rating> GetRatings(List<Movie> movieList, string title)
    {
        if (movieList.Count > 1)
            throw new ToManyMatchingRatingsException("Got " + movieList.Count + " matching Ratings for movie " + title +
                                                     ", but only 1 is allowed.");
        if (movieList.Count == 0) throw new NoMatchingRatingException("No matching Rating found for movie " + title);

        return movieList[0].Ratings.ToList();
    }
}