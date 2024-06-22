using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Models;
using MovieRating.TestProject.MockServices;
using MovieRating.Web.Controllers;
using MovieRating.Web.Models;

namespace MovieRating.TestProject.Web.Controller;

/// <summary>
/// Class <c>MovieControllerTest</c> contains unit tests for the <c>MovieController</c>.
/// </summary>
public class MovieControllerTest
{
    // Initialize mock services
    private static readonly MovieServiceMock MovieServiceMock = new();
    private static readonly InfoServiceMock InfoServiceMock = new();

    private readonly MovieController _movieController = new(MovieServiceMock, InfoServiceMock);

    private readonly Movie _movie1 = new()
    {
        Id = Guid.NewGuid(),
        Title = "Star Wars",
        Director = "Spielberg",
        Duration = "01:42",
        Genre = "Action",
        Description = "Long Long ago",
        Ratings = []
    };

    /// <summary>
    /// Method <c>TestInfoServiceResponse</c> tests the response of the InfoService for known and unknown movies.
    /// </summary>
    [Fact]
    public async Task TestInfoServiceResponse()
    {
        // Act
        var notFound = await _movieController.Index("unknown");
        var found = await _movieController.Index("Godzilla"); // Get a known movie from mock

        // Assert
        Assert.IsType<NotFoundResult>(notFound);

        var viewFound = Assert.IsType<ViewResult>(found);
        var modelFound = Assert.IsType<MovieDetailsViewModel>(viewFound.Model);
        Assert.Equal("MovieFromInfoMock", modelFound.SearchResult!.Title); // Assert the title matches the mock title
    }

    /// <summary>
    /// Method <c>TestInsertion</c> tests the insertion of a movie and retrieval by title.
    /// </summary>
    [Fact]
    public async Task TestInsertion()
    {
        // Act
        await MovieServiceMock.AddMovie(_movie1);
        var afterMovie1 = await _movieController.Index("Star Wars");

        var viewMovie1 = Assert.IsType<ViewResult>(afterMovie1);
        var modelMovie1 = Assert.IsType<MovieDetailsViewModel>(viewMovie1.Model);
        Assert.Equal("Star Wars", modelMovie1.SearchResult!.Title);
    }
}