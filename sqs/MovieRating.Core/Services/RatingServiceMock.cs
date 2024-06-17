using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Core.Services;

public class RatingServiceMock : IRatingService
{
    public Task<List<Rating>> ListAllRatings(string title)
    {
        throw new NotImplementedException();
    }

    public Task AddRating(Rating rating)
    {
        throw new NotImplementedException();
    }
}