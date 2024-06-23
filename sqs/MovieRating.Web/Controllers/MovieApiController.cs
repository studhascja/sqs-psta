using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;

namespace MovieRating.Web.Controllers;

/// <summary>
/// Class <c>MovieApiController</c> handles API requests for movies .
/// </summary>
[Route("/api/v1/Movie")]
[ApiController]
public class MovieApiController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IInfoService _infoService;

    /// <summary>
    /// Initializes a new instance of the <c>MovieApiController</c> class with the specified movie and info service.
    /// </summary>
    /// <param name="movieService">An instance of <c>IMovieService</c> to save Movies on and read them out of the database.</param>
    /// <param name="infoService">An instance of <c>IInfoService</c> to retrieve movie information from an external API.</param>
    public MovieApiController(IMovieService movieService, IInfoService infoService)
    {
        _movieService = movieService;
        _infoService = infoService;
    }

    /// <summary>
    /// Handles GET requests to retrieve a movie by its title.
    /// </summary>
    /// <param name="title">The title of the movie to retrieve.</param>
    /// <returns>A Movie or an 404 Status Code</returns>
    [HttpGet]
    [Route("{title}")]
    public async Task<IActionResult> GetMovieByName(string title)
    {
        if (await _movieService.DoesMovieExist(title))
        {
            var movie = await _movieService.GetMovieByTitle(title);
            return Ok(movie);
        }

        try
        {
            var movie = await _infoService.GetMovieInfo(title);
            await _movieService.AddMovie(movie);
            return Ok(movie);
        }
        catch (NoSuchMovieException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Handles GET requests to retrieve all movies saved in the database.
    /// </summary>
    /// <returns>The list of all movies saved in the database or a NotFound result if no movies are found.</returns>
    [HttpGet]
    public async Task<IActionResult> ListAll()
    {
        var movieList = await _movieService.ListAllMovies();
        if (movieList.Count == 0) return NotFound();
        return Ok(movieList);
    }
}