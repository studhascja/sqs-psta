using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRating.TestProject.MockServices;
using MovieRating.Web.Controllers;
using MovieRating.Web.Models;


namespace MovieRating.TestProject.Web.Controller;

public class HomeControllerTest
{
    private static readonly MovieServiceMock MovieServiceMock = new();
    private readonly HomeController _homeController = new(MovieServiceMock);

    [Fact]
    public void TestErrorHandling()
    {
        //Prep
        _homeController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        
        //Act
        var errorResult = _homeController.Error();

        //Assert
        var view = Assert.IsType<ViewResult>(errorResult);
        var model = Assert.IsType<ErrorViewModel>(view.Model);
        Assert.NotNull(model.RequestId);
        Assert.True(model.ShowRequestId);
        
    }
}