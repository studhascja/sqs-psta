using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Core.Services;

public class MovieServiceMock : IMovieService
{
    public Task<List<Movie>> ListAllMovies()
    {
        throw new NotImplementedException();
    }

    public Task<Movie> GetMovieByTitle(string title)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DoesMovieExist(string title)
    {
        throw new NotImplementedException();
    }

    public Task AddMovie(Movie movie)
    {
        throw new NotImplementedException();
    }
}