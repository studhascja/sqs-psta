using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Interfaces;
using MovieRating.Web.Models;

namespace MovieRating.Web.Controllers;

/// <summary>
/// Class <c>HomeController</c> is responsible for handling web requests and responses for the home page of the movie rating application.
/// </summary>
public class HomeController : Controller
{
    private readonly IMovieService _movieService;

    /// <summary>
    /// Initializes a new instance of the <c>HomeController</c> class with the specified movie service.
    /// </summary>
    /// <param name="movieService">An instance of <c>IMovieService</c> to save Movies on and read them out of the database.</param>
    public HomeController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    /// <summary>
    /// Asynchronously handles the request for the home page and returns a view with a list of movies.
    /// </summary>
    /// <returns>An <c>IActionResult</c> that renders the home page view with all Movies which are saved in the database.</returns>
    public async Task<IActionResult> Index()
    {
        var movieList = await _movieService.ListAllMovies();
        var viewModel = new MovieListViewModel
        {
            SavedMovies = movieList
        };

        return View(viewModel);
    }

    /// <summary>
    /// Handles error requests and returns an error view with details of the current request.
    /// </summary>
    /// <returns>An <c>IActionResult</c> that renders the error view.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}