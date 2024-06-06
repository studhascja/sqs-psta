namespace MovieRating.Core.Models;

public class Rating
{
    public int Id { get; set; }

    public string RatingNote { get; set; }

    public string Author { get; set; }

    public int Evaluation
    {
        get { return evaluation; }
        set
        {
            if (value < 1 || value > 10) throw new ArgumentOutOfRangeException("Rating only allowed 1 - 10");
            evaluation = value;
        }
    }

    private int evaluation;

    public Rating(int evaluation, int id, string ratingNote, string author)
    {
        Evaluation = evaluation;
        Id = id;
        RatingNote = ratingNote;
        Author = author;
    }
}