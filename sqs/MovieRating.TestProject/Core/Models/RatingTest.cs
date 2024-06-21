using MovieRating.Core.Models;

namespace MovieRating.TestProject.Core.Models;

public class RatingTest
{
    private static readonly Guid MovieTestId = Guid.Empty;
    private const string Title = "Godzilla";
    private const string Director = "Max";
    private const string Runtime = "120 min";
    private const string Genre = "Action";
    private const string Plot = "Große Echse";
    private static readonly List<Rating> TestRatingList= []; 
    
    private static readonly Guid TestId = Guid.Empty;
    private const string TestRatingNote = "spannend";
    private const string TestAuthor = "Max Mustermann";
    private const int TestEvaluation = 9;

    private static readonly Movie TestMovie = new()
    {
        Id = MovieTestId,
        Title = Title,
        Director = Director,
        Duration = Runtime,
        Genre = Genre,
        Description = Plot,
        Ratings = TestRatingList
    };
    
    private static readonly Rating TestRating = new Rating
    {
        Id = TestId,
        RatingNote = TestRatingNote,
        Author = TestAuthor,
        Evaluation = TestEvaluation,
        Movie = TestMovie
    };

    [Fact]
    public void GetRatingTest()
    {
        Assert.Equal(TestId, TestRating.Id);
        Assert.Equal(TestId, TestRating.Id);
        Assert.Equal(TestRatingNote, TestRating.RatingNote);
        Assert.Equal(TestAuthor, TestRating.Author);
        Assert.Equal(TestEvaluation, TestRating.Evaluation);
        Assert.Equal(MovieTestId, TestRating.Movie.Id);
    }
}