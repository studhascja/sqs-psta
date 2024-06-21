using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;
using MovieRating.Web.Models;

namespace MovieRating.Web.Controllers;

[ApiController]
[Route("Rating")]
public class RatingController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IRatingService _ratingService;
    
    public RatingController(IMovieService movieService, IRatingService ratingService)
    {
        _movieService = movieService;
        _ratingService = ratingService;
    }
    
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