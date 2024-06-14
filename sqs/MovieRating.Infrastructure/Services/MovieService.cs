using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Infrastructure.Services;

public class MovieService : IMovieService
{
    private MovieContext _movieContext;

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