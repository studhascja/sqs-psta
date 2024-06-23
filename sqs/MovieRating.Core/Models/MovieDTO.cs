namespace MovieRating.Core.Models;

/// <summary>
/// Class <c>MovieDTO</c> is a data transfer object (DTO) for transferring the information from the API to a <c>Movie</c> object.
/// </summary>
public class MovieDto
{
    /// <summary>
    /// Gets or sets the title of the movie.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the director of the movie.
    /// </summary>
    public required string Director { get; set; }

    /// <summary>
    /// Gets or sets the duration of the movie.
    /// </summary>
    public required string Runtime { get; set; }

    /// <summary>
    /// Gets or sets the genre of the movie.
    /// </summary>
    public required string Genre { get; set; }

    /// <summary>
    /// Gets or sets the description of the movie.
    /// </summary>
    public required string Plot { get; set; }
}