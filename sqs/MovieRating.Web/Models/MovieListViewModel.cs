using MovieRating.Core.Models;

namespace MovieRating.Web.Models;

public class MovieListViewModel
{
    public required List<Movie> SavedMovies { get; set; }
}
