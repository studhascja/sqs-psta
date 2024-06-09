using MovieRating.Core.Models;

namespace MovieRating.Core.Interfaces;

public interface IMovieService
{
    Task<List<Movie>> ListAllMovies();

    Task<bool> DoesMovieExist(string title);

    Task AddMovie(Movie movie);
}