using MovieRating.Core.Models;

namespace MovieRating.TestProject.Core.Models;

public class RatingTest
{
    private static readonly Guid TestId = Guid.Empty;
    private const string TestRatingNote = "spannend";
    private const string TestAuthor = "Max Mustermann";
    private const int TestEvaluation = 9;

    private static readonly Rating TestRating = new Rating
    {
        Id = TestId,
        RatingNote = TestRatingNote,
        Author = TestAuthor,
        Evaluation = TestEvaluation,
        Movie = null!
    };

    [Fact]
    public void GetRatingTest()
    {
        Assert.Equal(TestId, TestRating.Id);
        Assert.Equal(TestId, TestRating.Id);
        Assert.Equal(TestRatingNote, TestRating.RatingNote);
        Assert.Equal(TestAuthor, TestRating.Author);
        Assert.Equal(TestEvaluation, TestRating.Evaluation);
    }
}