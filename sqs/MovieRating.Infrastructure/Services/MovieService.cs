using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly MovieContext _movieContext;

    public MovieService(MovieContext movieContext)
    {
        _movieContext = movieContext;
    }

    public async Task<List<Movie>> ListAllMovies()
    {
        return await _movieContext.Movies
            .OrderBy(movie => movie.Title)
            .ToListAsync();
    }

    public async Task<Movie> GetMovieByTitle(string title)
    {
        return await _movieContext.Movies
            .Include(movie => movie.Ratings)
            .FirstAsync(movie => movie.Title == title);
    }

    public async Task<bool> DoesMovieExist(string title)
    {
        return await _movieContext.Movies
            .AnyAsync(movie => movie.Title == title);
    }

    public async Task AddMovie(Movie movie)
    {
        _movieContext.Movies.Add(movie);
        await _movieContext.SaveChangesAsync();
    }
}