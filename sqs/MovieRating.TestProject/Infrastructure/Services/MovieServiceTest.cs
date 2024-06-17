using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Models;
using MovieRating.Infrastructure;
using MovieRating.Infrastructure.Services;

namespace MovieRating.TestProject.Infrastructure.Services;

public class MovieServiceTest
{
    [Fact]
    public async Task ListAllMoviesTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder()
            .UseInMemoryDatabase(nameof(ListAllMoviesTest))
            .Options;

        await using var context = new MovieContext(dbContextOptions);
        var movieService = new MovieService(context);
        
        var movie1 = new Movie
        {
            Id = Guid.NewGuid(),
            Title = "Test Title 1",
            Director = "Test Director 1",
            Duration = "01:42",
            Genre = "Action",
            Description = "Test Description 1",
            Ratings = []
        };
        
        var movie2 = new Movie
        {
            Id = Guid.NewGuid(),
            Title = "Test Title 2",
            Director = "Test Director 2",
            Duration = "01:42",
            Genre = "Fantasy",
            Description = "Test Description 2",
            Ratings = []
        };
        
        // Act
        await movieService.AddMovie(movie1);
        var afterMovie1 = await movieService.ListAllMovies();

        await movieService.AddMovie(movie2);
        var afterMovie2 = await movieService.ListAllMovies();

        var check1 = await movieService.DoesMovieExist(movie1.Title);

        var check2 = await movieService.DoesMovieExist("unknown");
        
        //Assert
        var singleReview = Assert.Single(afterMovie1);
        Assert.Equal(movie1.Id, singleReview.Id);
        
        Assert.Equal(2, afterMovie2.Count);
        Assert.Contains(movie1, afterMovie2);
        Assert.Contains(movie2, afterMovie2);
        
        Assert.True(check1);
        Assert.False(check2);

    }
}