using MovieRating.Core.Models;

namespace MovieRating.Core.Interfaces;

public interface IMovieService
{
    List<Movie> ListAllMovies();

    bool DoesMovieExist(string title);

    void AddMovie(Movie movie);
}