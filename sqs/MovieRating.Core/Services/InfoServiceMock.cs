using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Core.Services;

public class InfoServiceMock : IInfoService
{
    public Task<Movie?> GetMovieInfo(string title)
    {
        throw new NotImplementedException();
    }
}