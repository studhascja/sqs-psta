namespace MovieRating.Core.Models;

public class Movie
{
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Director { get; set; }

    public required string Duration { get; set; }

    public required string Genre { get; set; }

    public required string Description { get; set; }

    public required List<Rating> Ratings { get; set; }
}