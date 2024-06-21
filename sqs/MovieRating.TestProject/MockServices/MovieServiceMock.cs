using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.TestProject.MockServices;

public class MovieServiceMock : IMovieService
{
    private readonly List<Movie> _movieList = new();

    public Task<List<Movie>> ListAllMovies()
    {
        return Task.FromResult(_movieList);
    }

    public Task<Movie> GetMovieByTitle(string title)
    {
        foreach (var movie in _movieList)
        {
            if (movie.Title == title) return Task.FromResult(movie);
        }

        throw new NoSuchMovieException("Movie is not in DB");
    }

    public Task<bool> DoesMovieExist(string title)
    {
        foreach (var movie in _movieList)
        {
            if (movie.Title == title) return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public Task AddMovie(Movie movie)
    {
        _movieList.Add(movie);
        return Task.CompletedTask;
    }
}