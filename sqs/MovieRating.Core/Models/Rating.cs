namespace MovieRating.Core.Models;

public class Rating
{
    public required Guid Id { get; set; }

    public required string RatingNote { get; set; }

    public required string Author { get; set; }

    public required int Evaluation { get; set; }
    public required Movie Movie { get; set; }
 }