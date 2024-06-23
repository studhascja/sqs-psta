using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Models;
using MovieRating.TestProject.MockServices;
using MovieRating.Web.Controllers;

namespace MovieRating.TestProject.Web.Controller;

/// <summary>
/// Class <c>MovieApiControllerTest</c> contains unit tests for the <c>MovieApiController</c>.
/// </summary>
public class MovieApiControllerTest
{
    /// <summary>
    /// Method <c>TestInsertion</c> tests the insertion of a movie and retrieval by title.
    /// </summary>
    [Fact]
    public async Task TestInsertion()
    {
        //Prep
        var movieServiceMock = new MovieServiceMock();
        var infoServiceMock = new InfoServiceMock();

        var movieApiController = new MovieApiController(movieServiceMock, infoServiceMock);

        var movie1 = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = "Star Wars",
            Director = "Spielberg",
            Duration = "01:42",
            Genre = "Action",
            Description = "Long Long ago",
            Ratings = []
        };

        //Act
        await movieServiceMock.AddMovie(movie1);
        var afterInsertion = await movieApiController.GetMovieByName(movie1.Title);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(afterInsertion);
        var movie = Assert.IsType<Movie>(okResult.Value);
        Assert.Equal(movie1.Id, movie.Id);
    }

    /// <summary>
    /// Method <c>TestInfoServiceResponse</c> tests the response of the InfoService for known and unknown movies.
    /// </summary>
    [Fact]
    public async Task TestInfoServiceResponse()
    {
        //Prep
        var movieServiceMock = new MovieServiceMock();
        var infoServiceMock = new InfoServiceMock();

        var movieApiController = new MovieApiController(movieServiceMock, infoServiceMock);

        //Act
        var notFound = await movieApiController.GetMovieByName("unknown"); // Try to get an unknown movie
        var mockMovie = await movieApiController.GetMovieByName("Godzilla"); // Get a known movie from mock

        //Assert
        Assert.IsType<NotFoundResult>(notFound);

        var okResult = Assert.IsType<OkObjectResult>(mockMovie);
        var movie = Assert.IsType<Movie>(okResult.Value);
        Assert.Equal("MovieFromInfoMock", movie.Title); // Assert the title matches the mock title
    }

    /// <summary>
    /// Method <c>TestListAllMovies</c> tests the listing of all movies in the service.
    /// </summary>
    [Fact]
    public async Task TestListAllMovies()
    {
        //Prep
        var movieServiceMock = new MovieServiceMock();
        var infoServiceMock = new InfoServiceMock();

        var movieApiController = new MovieApiController(movieServiceMock, infoServiceMock);

        var movie1 = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = "Star Wars",
            Director = "Spielberg",
            Duration = "01:42",
            Genre = "Action",
            Description = "Long Long ago",
            Ratings = []
        };

        var movie2 = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = "Herr der Ringe",
            Director = "Spielberg",
            Duration = "02:42",
            Genre = "Abenteuer",
            Description = "Große Füße",
            Ratings = []
        };

        //Act
        var notFound = await movieApiController.ListAll(); // Try to list movies when none are added

        await movieServiceMock.AddMovie(movie1);
        var afterMovie1 = await movieApiController.ListAll(); // List movies after adding the first movie

        //Assert
        Assert.IsType<NotFoundResult>(notFound);

        var okResult1 = Assert.IsType<OkObjectResult>(afterMovie1);
        var movieList1 = Assert.IsType<List<Movie>>(okResult1.Value);
        Assert.Single(movieList1);
        Assert.Contains(movie1, movieList1);

        //Act
        await movieServiceMock.AddMovie(movie2);
        var afterMovie2 = await movieApiController.ListAll(); // List movies after adding the second movie

        //Assert
        var okResult2 = Assert.IsType<OkObjectResult>(afterMovie2);
        var movieList2 = Assert.IsType<List<Movie>>(okResult2.Value);
        Assert.Equal(2, movieList2.Count); // Assert there are two movies in the list
        Assert.Contains(movie1, movieList2);
        Assert.Contains(movie2, movieList2);
    }
}