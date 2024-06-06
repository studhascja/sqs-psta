using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Core.Services;

public class MovieServiceMock : IMovieService
{
    public List<Movie> ListAllMovies()
    {
        throw new NotImplementedException();
    }

    public bool DoesMovieExist(string title)
    {
        throw new NotImplementedException();
    }

    public void AddMovie(Movie movie)
    {
        throw new NotImplementedException();
    }
}