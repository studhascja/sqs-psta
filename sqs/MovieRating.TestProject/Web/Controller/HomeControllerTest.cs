using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRating.TestProject.MockServices;
using MovieRating.Web.Controllers;
using MovieRating.Web.Models;


namespace MovieRating.TestProject.Web.Controller;

/// <summary>
/// Class <c>HomeControllerTest</c> contains unit tests for the <c>HomeController</c>.
/// </summary>
public class HomeControllerTest
{
    // Mock service for testing purposes
    private static readonly MovieServiceMock MovieServiceMock = new();
    private readonly HomeController _homeController = new(MovieServiceMock);

    /// <summary>
    /// Method <c>TestErrorHandling</c> tests the error handling logic.
    /// </summary>
    [Fact]
    public void TestErrorHandling()
    {
        //Prep
        _homeController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext() // Setting up a default HTTP context
        };

        //Act
        var errorResult = _homeController.Error(); // Invoke the Error method

        //Assert
        var view = Assert.IsType<ViewResult>(errorResult);
        var model = Assert.IsType<ErrorViewModel>(view.Model);
        Assert.NotNull(model.RequestId);
        Assert.True(model.ShowRequestId);
    }
}