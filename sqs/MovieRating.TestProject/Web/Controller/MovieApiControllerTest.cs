using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Models;
using MovieRating.TestProject.MockServices;
using MovieRating.Web.Controllers;

namespace MovieRating.TestProject.Web.Controller;

public class MovieApiControllerTest
{
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

    [Fact]
    public async Task TestInfoServiceResponse()
    {
        //Prep
        var movieServiceMock = new MovieServiceMock();
        var infoServiceMock = new InfoServiceMock();

        var movieApiController = new MovieApiController(movieServiceMock, infoServiceMock);

        //Act
        var notFound = await movieApiController.GetMovieByName("unknown");
        var mockMovie = await movieApiController.GetMovieByName("Godzilla");

        //Assert
        Assert.IsType<NotFoundResult>(notFound);

        var okResult = Assert.IsType<OkObjectResult>(mockMovie);
        var movie = Assert.IsType<Movie>(okResult.Value);
        Assert.Equal("MovieFromInfoMock", movie.Title);
    }

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
        var notFound = await movieApiController.ListAll();

        await movieServiceMock.AddMovie(movie1);
        var afterMovie1 = await movieApiController.ListAll();

        //Assert
        Assert.IsType<NotFoundResult>(notFound);

        var okResult1 = Assert.IsType<OkObjectResult>(afterMovie1);
        var movieList1 = Assert.IsType<List<Movie>>(okResult1.Value);
        Assert.Single(movieList1);
        Assert.Contains(movie1, movieList1);

        //Act
        await movieServiceMock.AddMovie(movie2);
        var afterMovie2 = await movieApiController.ListAll();

        //Assert
        var okResult2 = Assert.IsType<OkObjectResult>(afterMovie2);
        var movieList2 = Assert.IsType<List<Movie>>(okResult2.Value);
        Assert.Equal(2, movieList2.Count);
        Assert.Contains(movie1, movieList2);
        Assert.Contains(movie2, movieList2);
    }
}