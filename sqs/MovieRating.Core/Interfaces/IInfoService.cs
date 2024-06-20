using MovieRating.Core.Models;

namespace MovieRating.Core.Interfaces;

public interface IInfoService
{
    Task<Movie> GetMovieInfo(string title);
}