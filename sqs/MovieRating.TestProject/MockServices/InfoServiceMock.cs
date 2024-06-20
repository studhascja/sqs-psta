using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.TestProject.MockServices;

public class InfoServiceMock : IInfoService
{
    public Task<Movie> GetMovieInfo(string title)
    {
        if (title == "unknown") throw new NoSuchMovieException("TestMovie is unknown");
        return Task.FromResult(new Movie
        {
            Id = Guid.Empty,
            Title = "MovieFromInfoMock",
            Director = "Mock",
            Duration = "129 min",
            Genre = "Action",
            Description = "I`m mocked",
            Ratings = []
        });
    }
}