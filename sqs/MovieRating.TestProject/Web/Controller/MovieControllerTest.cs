using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Models;
using MovieRating.TestProject.MockServices;
using MovieRating.Web.Controllers;
using MovieRating.Web.Models;

namespace MovieRating.TestProject.Web.Controller;

public class MovieControllerTest
{
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
    
    [Fact]
    public async Task TestInfoServiceResponse()
    {
        // Act
        var notFound = await _movieController.Index("unknown");
        var found = await _movieController.Index("Godzilla");

        // Assert
        Assert.IsType<NotFoundResult>(notFound);
        
        var viewFound = Assert.IsType<ViewResult>(found);
        var modelFound = Assert.IsType<MovieDetailsViewModel>(viewFound.Model);
        Assert.Equal("MovieFromInfoMock", modelFound.SearchResult!.Title);
    }
    
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