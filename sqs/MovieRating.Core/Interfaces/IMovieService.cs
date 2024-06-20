using MovieRating.Core.Models;

namespace MovieRating.Core.Interfaces;

public interface IMovieService
{
    Task<List<Movie>> ListAllMovies();
    Task<Movie> GetMovieByTitle(string title);

    Task<bool> DoesMovieExist(string title);

    Task AddMovie(Movie movie);
}