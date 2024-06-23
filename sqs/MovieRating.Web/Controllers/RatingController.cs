using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;
using MovieRating.Web.Models;

namespace MovieRating.Web.Controllers;

/// <summary>
/// Class <c>RatingController</c> handles API requests for movie-ratings.
/// </summary>
[ApiController]
[Route("Rating")]
public class RatingController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IRatingService _ratingService;
    
    /// <summary>
    /// Initializes a new instance of the <c>RatingController</c> class with the specified movie and rating services.
    /// </summary>
    /// <param name="movieService">An instance of <c>IMovieService</c> to save Movies on and read them out of the database.</param>
    /// <param name="ratingService">An instance of <c>IRatingService</c> to save Ratings on and read them out of the database.</param>
    public RatingController(IMovieService movieService, IRatingService ratingService)
    {
        _movieService = movieService;
        _ratingService = ratingService;
    }
    
    /// <summary>
    /// Handles POST requests to add a rating to a movie.
    /// </summary>
    /// <param name="movieTitle">The title of the movie to add the rating to.</param>
    /// <param name="request">A <c>CreateRatingDto</c> object containing the rating details.</param>
    /// <returns>Redirects to the movie details page or returns a NotFound result if the movie does not exist.</returns>
    [HttpPost]
    [Route("{movieTitle}")]
    public async Task<IActionResult> AddRating(string movieTitle, [FromForm] CreateRatingDto request)
    {
        var movieExists = await _movieService.DoesMovieExist(movieTitle);
        if (!movieExists)
        {
            return NotFound();
        }

        var movie = await _movieService.GetMovieByTitle(movieTitle);
        var rating = new Rating
        {
            Id = Guid.NewGuid(),
            RatingNote = request.Rating,
            Author = request.Author,
            Evaluation = request.Evaluation,
            Movie = movie
        };
        await _ratingService.AddRating(rating);
        
        return Redirect($"/Movie?title={movieTitle}");
    }
}