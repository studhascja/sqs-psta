using MovieRating.Core.Models;
using MovieRating.Infrastructure.Services;

namespace MovieRating.TestProject.Infrastructure.Services;

/// <summary>
/// Class <c>InfoServiceTest</c> contains tests to ensure the InfoService behave as expected.
/// </summary>
public class InfoServiceTest
{
    // Prep
    private const string Title = "Godzilla";
    private const string Director = "Max";
    private const string Runtime = "120 min";
    private const string Genre = "Action";
    private const string Plot = "Große Echse";

    private static readonly MovieDto TestDto = new()
    {
        Title = Title,
        Director = Director,
        Runtime = Runtime,
        Genre = Genre,
        Plot = Plot
    };

    /// <summary>
    /// Tests if the function <c>ChangeToMovieDtoTest</c> converts correctly
    /// all Properties from the MovieDTO to the Movie
    /// </summary>
    [Fact]
    public void ChangeToMovieDtoTest()
    {
        // Act
        var result = InfoService.ChangeToMovieDto(TestDto);

        //Assert
        Assert.Equal(Title, result.Title);
        Assert.Equal(Director, result.Director);
        Assert.Equal(Genre, result.Genre);
        Assert.Empty(result.Ratings);
        Assert.Equal(Plot, result.Description);
        Assert.Equal(Runtime, result.Duration);
    }
}