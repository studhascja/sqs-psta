using MovieRating.Core.Models;

namespace MovieRating.Web.Models;

/// <summary>
/// Class <c>MovieDetailsViewModel</c> represents the model for displaying movie details.
/// </summary>
public class MovieDetailsViewModel
{
    /// <summary>
    /// Gets or sets the search result containing the movie details found.
    /// </summary>
    /// <value>A <c>Movie</c> object representing the search result.</value>
    public Movie? SearchResult { get; set; }
}
