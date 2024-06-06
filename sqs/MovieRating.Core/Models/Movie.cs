namespace MovieRating.Core.Models;

public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Director { get; set; }

    public double Duration { get; set; }

    public string Genre { get; set; }

    public string Description { get; set; }

    public List<Rating>? Ratings { get; set; }


    public Movie(int id, string title, string director, double duration, string genre, string description,
        List<Rating>? ratings)
    {
        Id = id;
        Title = title;
        Director = director;
        Duration = duration;
        Genre = genre;
        Description = description;
        Ratings = ratings;
    }
}