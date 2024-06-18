using MovieRating.Core.Models;
using MovieRating.Infrastructure.Services;

namespace MovieRating.TestProject.Infrastructure.Services;

public class InfoServiceTest
{
    private const string Title = "Godzilla";
    private const string Director = "Max";
    private const string Runtime = "120 min";
    private const string Genre = "Action";
    private const string Plot = "Große Echse";

    private static readonly InfoService TestInfoService = new("test");
    private static readonly MovieDto TestDto = new()
    {
        Title = Title,
        Director = Director,
        Runtime = Runtime,
        Genre = Genre,
        Plot = Plot
    };
    
    [Fact]
    public void ChangeToMovieDtoTest()
    {
        var result = TestInfoService.ChangeToMovieDto(TestDto);
        
        Assert.Equal(Title, result.Title);
        Assert.Equal(Director, result.Director);
        Assert.Equal(Genre, result.Genre);
        Assert.Empty(result.Ratings);
        Assert.Equal(Plot, result.Description);
        Assert.Equal(Runtime, result.Duration);
    }
}