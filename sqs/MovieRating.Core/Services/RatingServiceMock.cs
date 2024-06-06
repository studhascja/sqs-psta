using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Core.Services;

public class RatingServiceMock : IRatingService
{
    public List<Rating> ListAllRatings(Movie movie)
    {
        throw new NotImplementedException();
    }

    public void AddRating(Movie movie, Rating rating)
    {
        throw new NotImplementedException();
    }
}