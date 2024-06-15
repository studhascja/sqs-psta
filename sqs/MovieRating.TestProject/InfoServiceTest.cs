using MovieRating.Core.Models;
using MovieRating.Web.Services;

namespace MovieRating.TestProject;

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
        
        Assert.True(result.Title == Title);
        Assert.True(result.Director == Director);
        Assert.True(result.Genre == Genre);
        Assert.True(result.Ratings == null);
        Assert.True(result.Description == Plot);
        Assert.True(result.Duration == Runtime);
    }
}