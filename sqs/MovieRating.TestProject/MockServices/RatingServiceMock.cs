using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.TestProject.MockServices;

/// <summary>
/// Class <c>RatingServiceMock</c> is a mock implementation of the <c>IRatingService</c> interface for testing purposes.
/// </summary>
public class RatingServiceMock : IRatingService
{
    private readonly List<Movie> _movieList = new();

    /// <summary>
    /// Method <c>ListAllRatings</c> returns a list of all ratings for a specified movie title.
    /// </summary>
    /// <param name="title">The title of the movie to search for.</param>
    /// <returns>Returns asynchronously a list of <c>Rating</c> objects.</returns>
    /// <exception cref="ToManyMatchingRatingsException">Thrown when there are multiple movies with the same title.</exception>
    /// <exception cref="NoMatchingRatingException">Thrown when no ratings are found for the specified movie title.</exception>
    public Task<List<Rating>> ListAllRatings(string title)
    {
        var matchingMovies = new List<Movie>();
        foreach (var movie in _movieList)
        {
            if (movie.Title == title) matchingMovies.Add(movie);
        }

        return Task.FromResult(GetRatings(matchingMovies, title));
    }

    /// <summary>
    /// Method <c>AddRating</c> adds a new rating to the corresponding movie.
    /// </summary>
    /// <param name="rating">The <c>Rating</c> object to add.</param>
    public Task AddRating(Rating rating)
    {
        foreach (var movie in _movieList)
        {
            if (movie.Id == rating.Movie.Id)
            {
                movie.Ratings.Add(rating);
                return Task.CompletedTask;
            }
        }

        AddMovie(rating.Movie);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Method <c>AddMovie</c> adds a new movie to the list.
    /// </summary>
    /// <param name="movie">The <c>Movie</c> object to add.</param>
    public void AddMovie(Movie movie)
    {
        _movieList.Add(movie);
    }

    /// <summary>
    /// Method <c>GetRatings</c> retrieves ratings for a movie based on its title.
    /// </summary>
    /// <param name="movieList">The list of movies that match the specified title.</param>
    /// <param name="title">The title of the movie to search for.</param>
    /// <returns>Returns a list of <c>Rating</c> objects.</returns>
    /// <exception cref="ToManyMatchingRatingsException">Thrown when there are multiple movies with the same title.</exception>
    /// <exception cref="NoMatchingRatingException">Thrown when no movies are found for the specified movie title.</exception>
    private static List<Rating> GetRatings(List<Movie> movieList, string title)
    {
        if (movieList.Count > 1)
            throw new ToManyMatchingRatingsException("Got " + movieList.Count + " matching Ratings for movie " + title +
                                                     ", but only 1 is allowed.");
        if (movieList.Count == 0) throw new NoMatchingRatingException("No matching Rating found for movie " + title);

        return movieList[0].Ratings.ToList();
    }
}