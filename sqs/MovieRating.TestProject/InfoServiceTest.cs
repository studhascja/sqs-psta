using MovieRating.Core.Models;
using MovieRating.Web.Services;

namespace MovieRating.TestProject;

public class InfoServiceTest
{
    private static string _title = "Godzilla";
    private static string _director = "Max";
    private static string _runtime = "120 min";
    private static string _genre = "Action";
    private static string _plot = "Große Echse";
    
    private static InfoService _testInfoService = new InfoService();
    private static MovieDto _testDto = new MovieDto
    {
        Title = _title,
        Director = _director,
        Runtime = _runtime,
        Genre = _genre,
        Plot = _plot
    };
    
    [Fact]
    public void ChangeToMovieDtoTest()
    {
        Movie result = _testInfoService.ChangeToMovieDto(_testDto);
        
        Assert.True(result.Title == _title);
        Assert.True(result.Director == _director);
        Assert.True(result.Genre == _genre);
        Assert.True(result.Ratings == null);
        Assert.True(result.Description == _plot);
        Assert.True(result.Duration == _runtime);
    }
}