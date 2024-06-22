using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;
using MovieRating.Web.Models;

namespace MovieRating.Web.Controllers;

/// <summary>
/// Class <c>MovieController</c> handles web requests and responses for movies.
/// </summary>
public class MovieController : Controller
{
    private readonly IMovieService _movieService;
    private readonly IInfoService _infoService;

    /// <summary>
    /// Initializes a new instance of the <c>MovieController</c> class with the specified movie and info services.
    /// </summary>
    /// <param name="movieService">An instance of <c>IMovieService</c> to save Movies on and read them out of the database.</param>
    /// <param name="infoService">An instance of <c>IInfoService</c> to retrieve movie information from an external API.</param>
    public MovieController(IMovieService movieService, IInfoService infoService)
    {
        _movieService = movieService;
        _infoService = infoService;
    }

    /// <summary>
    /// Handles GET requests to retrieve movie details by title.
    /// </summary>
    /// <param name="title">The title of the movie to retrieve.</param>
    /// <returns> The movie details or a NotFound result if the movie does not exist.</returns>
    public async Task<IActionResult> Index(string title)
    {
        var movieExists = await _movieService.DoesMovieExist(title);
        Movie? result;

        if (movieExists)
        {
            result = await _movieService.GetMovieByTitle(title);
        }
        else
        {
            try
            {
                var apiResult = await _infoService.GetMovieInfo(title);
                await _movieService.AddMovie(apiResult);
                result = apiResult;
            }
            catch(NoSuchMovieException)
            {
                return NotFound();
            }
        }
        
        var viewModel = new MovieDetailsViewModel
        {
            SearchResult = result
        };
        
        if (ModelState.IsValid)
        {
            return View(viewModel);
        }

        return NotFound();

    }
}