﻿using Microsoft.AspNetCore.Mvc;
using MovieRating.Core.Models;
using MovieRating.TestProject.MockServices;
using MovieRating.Web.Controllers;
using MovieRating.Web.Models;

namespace MovieRating.TestProject.Web.Controller;

public class RatingControllerTest
{
    private static readonly MovieServiceMock MovieServiceMock = new();
    private static readonly RatingServiceMock RatingServiceMock = new();
    private readonly RatingController _ratingController = new(MovieServiceMock, RatingServiceMock);

    private readonly Movie _movie1 = new()
    {
        Id = Guid.NewGuid(),
        Title = "Star Wars",
        Director = "Spielberg",
        Duration = "01:42",
        Genre = "Action",
        Description = "Long Long ago",
        Ratings = []
    };

    private readonly CreateRatingDto _ratingDto1 = new();
    private readonly CreateRatingDto _ratingDto2 = new();
    [Fact]
    public async Task AddRatingTest()
    {
        //Act
        _ratingDto1.Author = "Thor";
        _ratingDto1.Rating = "spannend";
        _ratingDto1.Evaluation = 5;
        
        _ratingDto2.Author = "Zeus";
        _ratingDto2.Rating = "aufregend";
        _ratingDto2.Evaluation = 9;
        
        await MovieServiceMock.AddMovie(_movie1);
        RatingServiceMock.AddMovie(_movie1);
        
        var afterRating1 = await _ratingController.AddRating(_movie1.Title, _ratingDto1) as RedirectResult;
        var countAfterRating1 = await RatingServiceMock.ListAllRatings(_movie1.Title);
        
        //Assert
        Assert.NotNull(afterRating1);
        Assert.Equal($"/Movie?title=" + _movie1.Title, afterRating1.Url);
        Assert.Single(countAfterRating1);
        
        var afterRating2 = await _ratingController.AddRating(_movie1.Title, _ratingDto2) as RedirectResult;
        var countAfterRating2 = await RatingServiceMock.ListAllRatings(_movie1.Title);
        Assert.NotNull(afterRating2);
        Assert.Equal($"/Movie?title=" + _movie1.Title, afterRating2.Url);
        Assert.Equal(2, countAfterRating2.Count);
    }

    [Fact]
    public async Task TestWithoutMovie()
    {
        //Act
        _ratingDto1.Author = "Thor";
        _ratingDto1.Rating = "spannend";
        _ratingDto1.Evaluation = 5;
        var notFound = await _ratingController.AddRating("unknown", _ratingDto1);
        
        // Assert
        Assert.IsType<NotFoundResult>(notFound);
    }
}