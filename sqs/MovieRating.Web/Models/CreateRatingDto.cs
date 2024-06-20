namespace MovieRating.Web.Models;

public class CreateRatingDto
{
    public required string Author { get; set; }
    public required string Rating { get; set; }

    public required int Evaluation { get; set; }
}