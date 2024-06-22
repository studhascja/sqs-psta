using MovieRating.Core.Models;

namespace MovieRating.TestProject.Core.Models;

/// <summary>
/// Class <c>MovieTest</c> contains tests to test the Movie Model.
/// </summary>
public class MovieTest
{
    // Prep 
    private static readonly Guid TestId = Guid.Empty;
    private const string Title = "Godzilla";
    private const string Director = "Max";
    private const string Runtime = "120 min";
    private const string Genre = "Action";
    private const string Plot = "Große Echse";
    private static readonly List<Rating> TestRatingList = [];

    private const string TestRatingNote = "spannend";
    private const string TestAuthor = "Max Mustermann";
    private const int TestEvaluation = 9;

    // Act
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
        Evaluation = TestEvaluation,
        Movie = TestMovie
    };

    /// <summary>
    /// Tests Getter and Setter of the MovieId
    /// </summary>
    [Fact]
    public void MovieIdTest()
    {
        // Assert
        Assert.Equal(TestId, TestMovie.Id);
    }

    /// <summary>
    /// Tests Getter and Setter of the Movie Rating
    /// </summary>
    [Fact]
    public void MovieRatingTest()
    {
        //Assert
        Assert.Equal(TestRatingList, TestMovie.Ratings);
        TestMovie.Ratings.Add(TestRating);
        Assert.Equal(TestRating, TestMovie.Ratings[0]);
    }
}