﻿using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Web.Services;

public class InfoService : IInfoService
{
    private string _apiKey;

    public InfoService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<Movie?> GetMovieInfo(string title)
    {
        var client = new HttpClient();
        var response = await client.GetFromJsonAsync<MovieDto>($"http://www.omdbapi.com/?apikey={_apiKey}&t={title}");

        return ChangeToMovieDto(response ?? throw new InvalidOperationException());
    }

    public Movie ChangeToMovieDto(MovieDto movieDto)
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = movieDto.Title,
            Director = movieDto.Director,
            Duration = movieDto.Runtime,
            Genre = movieDto.Genre,
            Description = movieDto.Plot
        };
    }
}