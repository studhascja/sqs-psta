using System.Net;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Models;
using MovieRating.Infrastructure;
using MovieRating.Infrastructure.Services;


namespace MovieRating.IntegrationTests;

/// <summary>
/// Integration tests for various scenarios related to movie pages and error handling.
/// </summary>
public partial class TestPageScenarios : IClassFixture<TestingWebAppFactory>
{
    private readonly TestingWebAppFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <c>TestPageScenarios</c> class with the specified TestingWebAppFactory factory.
    /// </summary>
    /// <param name="factory">An instance of <c>TestingWebAppFactory</c> to create a Client with which the Program can be tested.</param>
    public TestPageScenarios(TestingWebAppFactory factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Tests if the Startpage is available and the Movies are displayed as expected
    /// </summary>
    [Fact]
    public async Task TestStartPage()
    {
        // Prep
        var client = _factory.CreateClient();
        var dbContextOptions = new DbContextOptionsBuilder()
            .UseInMemoryDatabase(nameof(TestingWebAppFactory))
            .Options;

        await using var context = new MovieContext(dbContextOptions);
        var movieService = new MovieService(context);

        var movie1 = new Movie
        {
            Id = Guid.NewGuid(),
            Title = "Godzilla",
            Director = "Test Director 1",
            Duration = "01:42",
            Genre = "Action",
            Description = "Test Description 1",
            Ratings = []
        };
        var movie2 = new Movie
        {
            Id = Guid.NewGuid(),
            Title = "Avatar",
            Director = "Test Director 1",
            Duration = "01:42",
            Genre = "Action",
            Description = "Test Description 1",
            Ratings = []
        };

        // Act
        await movieService.AddMovie(movie1);
        await movieService.AddMovie(movie2);
        var response = await client.GetAsync("/");

        var content = await response.Content.ReadAsStringAsync();
        var matches = HomeRegex().Matches(content);
        var movieTitleAvatar = matches[0].Groups["titleText"].Value.Trim(); // Grab the Movie Titles with a Regex
        var movieTitleGodzilla = matches[1].Groups["titleText"].Value.Trim(); // Test if alphabetic sorted (A before G)

        // Assert
        response.EnsureSuccessStatusCode(); // checks if it is an non Error Status Code
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType!.ToString());

        Assert.Equal(movie1.Title, movieTitleGodzilla);
        Assert.Equal(movie2.Title, movieTitleAvatar);
    }

    /// <summary>
    /// Tests if the MoviePage is available and th Ratings are displayed correctly
    /// </summary>
    [Fact]
    public async Task TestMoviePageWithMultipleRatings()
    {
        // Prep
        var client = _factory.CreateClient();
        var dbContextOptions = new DbContextOptionsBuilder()
            .UseInMemoryDatabase(nameof(TestingWebAppFactory))
            .Options;

        await using var context = new MovieContext(dbContextOptions);
        var movieService = new MovieService(context);
        var ratingService = new RatingService(context);

        var movie1 = new Movie
        {
            Id = Guid.NewGuid(),
            Title = "Godzilla",
            Director = "Test Director 1",
            Duration = "01:42",
            Genre = "Action",
            Description = "Test Description 1",
            Ratings = []
        };

        var rating1 = new Rating
        {
            Id = Guid.NewGuid(),
            RatingNote = "super",
            Author = "Me",
            Evaluation = 8,
            Movie = movie1
        };

        var rating2 = new Rating
        {
            Id = Guid.NewGuid(),
            RatingNote = "cool",
            Author = "He",
            Evaluation = 9,
            Movie = movie1
        };

        //Act
        await movieService.AddMovie(movie1);
        var response = await client.GetAsync("/Movie?title=Godzilla"); // Check if the Godzilla Movie Page is available 

        await ratingService.AddRating(rating1);
        var responseAfterRating1 = await client.GetAsync("/Movie?title=Godzilla");
        var contentAfterRating1 = await responseAfterRating1.Content.ReadAsStringAsync();
        var matchesAfterRating1 = MovieRegex().Matches(contentAfterRating1);

        await ratingService.AddRating(rating2);
        var responseAfterRating2 = await client.GetAsync("/Movie?title=Godzilla");
        var contentAfterRating2 = await responseAfterRating2.Content.ReadAsStringAsync();
        var matchesAfterRating2 = MovieRegex().Matches(contentAfterRating2);
        var rating1RatingNote =
            matchesAfterRating2[0].Groups["RatingNote"].Value
                .Trim(); // check via Regex that all Ratings are displayed correctly
        var rating1Evaluation = matchesAfterRating2[0].Groups["Evaluation"].Value.Trim();

        var rating2RatingNote = matchesAfterRating2[1].Groups["RatingNote"].Value.Trim();
        var rating2Evaluation = matchesAfterRating2[1].Groups["Evaluation"].Value.Trim();

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType!.ToString());

        Assert.Single(matchesAfterRating1);

        Assert.Equal(2, matchesAfterRating2.Count);

        Assert.Equal(rating1.RatingNote, rating1RatingNote);
        Assert.Equal(rating1.Evaluation, int.Parse(rating1Evaluation));

        Assert.Equal(rating2.RatingNote, rating2RatingNote);
        Assert.Equal(rating2.Evaluation, int.Parse(rating2Evaluation));
    }

    /// <summary>
    /// Tests if the Moviepage is available although it should not
    /// </summary>
    /// 
    [Fact]
    public async Task TestMoviePageWithoutMovie()
    {
        // Prep
        var client = _factory.CreateClient();

        //Act
        var response = await client.GetAsync("/Movie?title=unknownMovie"); // Movie does not exist

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// Tests if the ErrorPage is available
    /// </summary>
    [Fact]
    public async Task TestErrorPage()
    {
        // Prep
        var client = _factory.CreateClient();

        //Act
        var response = await client.GetAsync("/Home/Error");

        //Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType!.ToString());
    }

    [GeneratedRegex("""<a href="Movie\?title=(?<title>[^"]*)">(?<titleText>[^<]*)</a>""", RegexOptions.IgnoreCase,
        "de-DE")]
    private static partial Regex HomeRegex();

    [GeneratedRegex(@"<li><b>Review:</b>\s*(?<RatingNote>.*?)\s*<b>Evaluation:</b>\s*(?<Evaluation>\d+)</li>",
        RegexOptions.IgnoreCase, "de-DE")]
    private static partial Regex MovieRegex();
}