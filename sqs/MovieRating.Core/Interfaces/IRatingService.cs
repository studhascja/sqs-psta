using MovieRating.Core.Models;

namespace MovieRating.Core.Interfaces;

public interface IRatingService
{
    Task<List<Rating>> ListAllRatings(string title);
    
    Task AddRating(Movie movie, Rating rating);
}