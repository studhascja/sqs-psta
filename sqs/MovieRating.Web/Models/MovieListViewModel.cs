using MovieRating.Core.Models;

namespace MovieRating.Web.Models;

/// <summary>
/// Class <c>MovieListViewModel</c> represents the model for displaying a list of saved movies.
/// </summary>
public class MovieListViewModel
{
    /// <summary>
    /// Gets or sets the list of saved movies.
    /// </summary>
    /// <value>A list of <c>Movie</c> objects representing the saved movies.</value>
    public required List<Movie> SavedMovies { get; set; }
}
