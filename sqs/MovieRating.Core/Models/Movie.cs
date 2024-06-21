namespace MovieRating.Core.Models;

/// <summary>
/// Class <c>Movie</c>Represents a movie entity.
/// </summary>
public class Movie
{
    /// <summary>
    /// Gets or sets the unique identifier of the movie.
    /// </summary>
    public required Guid Id { get; set; }

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
    public required string Duration { get; set; }

    /// <summary>
    /// Gets or sets the genre of the movie.
    /// </summary>
    public required string Genre { get; set; }

    /// <summary>
    /// Gets or sets the description of the movie.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the list of ratings given to the movie.
    /// </summary>
    public required List<Rating> Ratings { get; set; }
}