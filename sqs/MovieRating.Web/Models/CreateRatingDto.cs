namespace MovieRating.Web.Models;

/// <summary>
/// Class <c>CreateRatingDto</c> represents the data transfer object for creating a new rating.
/// </summary>
public class CreateRatingDto
{
    /// <summary>
    /// Gets or sets the author of the rating.
    /// </summary>
    /// <value>The author of the rating.</value>
    public required string Author { get; set; }

    /// <summary>
    /// Gets or sets the rating note.
    /// </summary>
    /// <value>The rating note as a string.</value>
    public required string Rating { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score of the rating.
    /// </summary>
    /// <value>The evaluation score as an integer.</value>
    public required int Evaluation { get; set; }
}