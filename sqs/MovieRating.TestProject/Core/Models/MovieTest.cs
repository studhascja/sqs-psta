using MovieRating.Core.Models;

namespace MovieRating.TestProject.Core.Models;

public class MovieTest
{
    private static readonly Guid TestId = Guid.Empty;
    private const string Title = "Godzilla";
    private const string Director = "Max";
    private const string Runtime = "120 min";
    private const string Genre = "Action";
    private const string Plot = "Große Echse";
    private static readonly List<Rating> TestRatingList= []; 
    
    private const string TestRatingNote = "spannend";
    private const string TestAuthor = "Max Mustermann";
    private const int TestEvaluation = 9;
    
    private static readonly Movie TestMovie = new()
    {
        Id = TestId,
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
        Evaluation = TestEvaluation
    };
    
    [Fact]
    public void MovieIdTest()
    {
        Assert.Equal(TestId, TestMovie.Id);
    }

    [Fact]
    public void MovieRatingTest()
    {
        Assert.Equal(TestRatingList, TestMovie.Ratings);
        TestMovie.Ratings.Add(TestRating);
        Assert.Equal(TestRating, TestMovie.Ratings[0]);
    }
}