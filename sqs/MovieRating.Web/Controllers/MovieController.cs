using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;
using MovieRating.Web.Models;

namespace MovieRating.Web.Controllers;

public class MovieController : Controller
{
    private readonly IMovieService _movieService;
    private readonly IInfoService _infoService;

    public MovieController(IMovieService movieService, IInfoService infoService)
    {
        _movieService = movieService;
        _infoService = infoService;
    }

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
            var apiResult = await _infoService.GetMovieInfo(title);
            if (apiResult == null)
            {
                return NotFound();
            }
            
            await _movieService.AddMovie(apiResult);

            result = apiResult;
        }
        
        var viewModel = new MovieDetailsViewModel
        {
            SearchResult = result
        };
        
        return View(viewModel);
    }
}