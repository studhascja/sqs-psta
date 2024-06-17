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
        Evaluation = TestEvaluation
    };

    [Fact]
    public void GetRatingTest()
    {
        Assert.True(TestRating.Id.Equals(TestId));
        Assert.True(TestRating.RatingNote == TestRatingNote);
        Assert.True(TestRating.Author == TestAuthor);
        Assert.True(TestRating.Evaluation == TestEvaluation);
    }
}