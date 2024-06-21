using MovieRating.Core.Models;

namespace MovieRating.Core.Interfaces;

/// <summary>
/// Interface <c>IRatingService</c> provides methods to manage movie ratings.
/// </summary>
public interface IRatingService
{
    /// <summary>
    /// Method <c>ListAllRatings</c> lists all ratings for a specific movie which are in the database.
    /// </summary>
    /// <param name="title">The title of the movie.</param>
    /// <returns>Returns asynchronously a list of <c>Rating</c> objects.</returns>
    Task<List<Rating>> ListAllRatings(string title);

    /// <summary>
    /// Method <c>AddRating</c> adds a new rating for a movie.
    /// </summary>
    /// <param name="rating">The <c>Rating</c> object to be added.</param>
    Task AddRating(Rating rating);
}