using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.TestProject.MockServices;

public class RatingServiceMock : IRatingService
{
    private readonly List<Movie> _movieList = new();

    public Task<List<Rating>> ListAllRatings(string title)
    {
        var matchingMovies = new List<Movie>();
        foreach (var movie in _movieList)
        {
            if (movie.Title == title) matchingMovies.Add(movie);
        }

        return Task.FromResult(GetRatings(matchingMovies, title));
    }

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

    public void AddMovie(Movie movie)
    {
        _movieList.Add(movie);
    }

    private List<Rating> GetRatings(List<Movie> movieList, string title)
    {
        if (movieList.Count > 1)
            throw new ToManyMatchingRatingsException("Got " + movieList.Count + " matching Ratings for movie " + title +
                                                     ", but only 1 is allowed.");
        if (movieList.Count == 0) throw new NoMatchingRatingException("No matching Rating found for movie " + title);

        return movieList[0].Ratings.ToList();
    }
}