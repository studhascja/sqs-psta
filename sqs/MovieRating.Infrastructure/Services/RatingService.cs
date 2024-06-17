using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Infrastructure.Services;

public class RatingService : IRatingService
{
    private MovieContext _movieContext;

    public RatingService(MovieContext movieContext)
    {
        _movieContext = movieContext;
    }

    public async Task<List<Rating>> ListAllRatings(string title)
    {
        var movieList = await _movieContext.Movies
            .Where(movie => movie.Title == title)
            .Include(movie => movie.Ratings)
            .ToListAsync();

        return GetRatings(movieList, title);
    }

    public async Task AddRating(Movie movie, Rating rating)
    {
        _movieContext.Ratings.Add(rating);

        await _movieContext.SaveChangesAsync();
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