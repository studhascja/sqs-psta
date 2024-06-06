using MovieRating.Core.Models;

namespace MovieRating.Core.Interfaces;

public interface IInfoService
{
    Movie GetMovieInfo(string title);
}