using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Exceptions;
using MovieRating.Core.Models;
using MovieRating.Infrastructure;
using MovieRating.Infrastructure.Services;

namespace MovieRating.TestProject.Infrastructure.Services;

/// <summary>
/// Class <c>RatingServiceTest</c> contains tests to ensure the RatingService behave as expected.
/// </summary>
public class RatingServiceTest
{
    /// <summary>
    /// Test all functions of the RatingService
    /// </summary>
    [Fact]
    public async Task GetRatingsTest()
    {
        // Prep
        var dbContextOptions = new DbContextOptionsBuilder()
            .UseInMemoryDatabase(nameof(GetRatingsTest))
            .Options;

        await using var context = new MovieContext(dbContextOptions);
        var ratingService = new RatingService(context);

        var expectedMovie = new Movie
        {
            Id = Guid.NewGuid(),
            Title = "Test Title",
            Director = "Test Director",
            Duration = "01:42",
            Genre = "Action",
            Description = "Test Description",
            Ratings = []
        };

        var testMovie = new Movie
        {
            Id = Guid.NewGuid(),
            Title = "Test Title",
            Director = "Test Director",
            Duration = "01:45",
            Genre = "Action",
            Description = "Test Description",
            Ratings = []
        };

        var rating1 = new Rating
        {
            Id = Guid.NewGuid(),
            RatingNote = "Test Note",
            Author = "Test Author",
            Evaluation = 3,
            Movie = expectedMovie
        };

        var rating2 = new Rating
        {
            Id = Guid.NewGuid(),
            RatingNote = "Test Note 2",
            Author = "Test Author 2",
            Evaluation = 7,
            Movie = expectedMovie
        };

        var rating3 = new Rating
        {
            Id = Guid.NewGuid(),
            RatingNote = "Test Note 3",
            Author = "Test Author 3",
            Evaluation = 9,
            Movie = testMovie
        };

        // Act
        await ratingService.AddRating(rating1);
        var after1Rating = await ratingService.ListAllRatings(expectedMovie.Title); // One Rating

        await ratingService.AddRating(rating2);
        var after2Ratings = await ratingService.ListAllRatings(expectedMovie.Title); // Two Ratings

        await ratingService.AddRating(rating3);

        // Assert
        var singleReview = Assert.Single(after1Rating);
        Assert.Equal(rating1.Id, singleReview.Id);

        Assert.Equal(2, after2Ratings.Count);
        Assert.NotEqual(after2Ratings[0].Id, after2Ratings[1].Id);

        await Assert.ThrowsAsync<NoMatchingRatingException>(async () =>
            await ratingService.ListAllRatings("unknown")); // Ratings of a not existing Movie

        await Assert.ThrowsAsync<ToManyMatchingRatingsException>(async () =>
            await ratingService.ListAllRatings(testMovie.Title)); // Two Movies with the same Name
    }
}