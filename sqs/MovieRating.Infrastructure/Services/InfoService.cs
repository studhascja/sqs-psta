﻿using System.Net.Http.Json;
using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.Infrastructure.Services;

public class InfoService : IInfoService
{
    private readonly string? _apiKey;
    
    public InfoService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<Movie?> GetMovieInfo(string title)
    {
        using var client = new HttpClient();
        var response = await client.GetFromJsonAsync<MovieDto>($"https://www.omdbapi.com/?apikey={_apiKey}&t={title}");

        return ChangeToMovieDto(response ?? throw new NoSuchMovieException("Movie " + title + "does not exist."));
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
            Description = movieDto.Plot,
            Ratings = []
        };
    }
}