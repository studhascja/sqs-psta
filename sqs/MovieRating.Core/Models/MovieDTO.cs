namespace MovieRating.Core.Models;

public class MovieDto
{
    public required string Title { get; set; }

    public required string Director { get; set; }

    public required string Runtime { get; set; }

    public required string Genre { get; set; }

    public required string Plot { get; set; }
}