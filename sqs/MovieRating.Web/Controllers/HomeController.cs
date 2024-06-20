using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Interfaces;
using MovieRating.Web.Models;

namespace MovieRating.Web.Controllers;

public class HomeController : Controller
{
    private readonly IMovieService _movieService;

    public HomeController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {
        var movieList = await _movieService.ListAllMovies();
        var viewModel = new MovieListViewModel
        {
            SavedMovies = movieList
        };
        
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}