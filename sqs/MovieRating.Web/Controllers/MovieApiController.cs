using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;

namespace MovieRating.Web.Controllers;

[Route("/api/v1/Movie")]
[ApiController]
public class MovieApiController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IInfoService _infoService;

    public MovieApiController(IMovieService movieService, IInfoService infoService)
    {
        _movieService = movieService;
        _infoService = infoService;
    }

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

    [HttpGet]
    public async Task<IActionResult> ListAll()
    {
        var movieList = await _movieService.ListAllMovies();
        if (movieList.Count == 0) return NotFound(movieList);
        return Ok(movieList);
    }
}