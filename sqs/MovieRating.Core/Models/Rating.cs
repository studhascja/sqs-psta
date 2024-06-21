namespace MovieRating.Core.Models;

/// <summary>
/// Class <c>Rating</c> represents a rating given to a movie.
/// </summary>
public class Rating
{
    /// <summary>
    /// Gets or sets a unique identifier of the rating.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the note or comment associated with the rating.
    /// </summary>
    public required string RatingNote { get; set; }

    /// <summary>
    /// Gets or sets the author of the rating.
    /// </summary>
    public required string Author { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score given in the rating.
    /// </summary>
    public required int Evaluation { get; set; }

    /// <summary>
    /// Gets or sets the movie which is rated
    /// </summary>
    public required Movie Movie { get; set; }
}