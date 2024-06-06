using MovieRating.Core.Models;

namespace MovieRating.Core.Interfaces;

public interface IRatingService
{
    List<Rating> ListAllRatings(Movie movie);
    
    void AddRating(Movie movie, Rating rating);
}