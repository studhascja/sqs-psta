using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;
using MovieRating.Core.Models;

namespace MovieRating.TestProject.MockServices;

/// <summary>
/// Class <c>InfoServiceMock</c> is a mock implementation of the <c>IInfoService</c> interface for testing purposes.
/// </summary>
public class InfoServiceMock : IInfoService
{
    /// <summary>
    /// Method <c>GetMovieInfo</c> simulates retrieving movie information based on the provided title.
    /// </summary>
    /// <param name="title">The title of the movie to search for.</param>
    /// <returns>Returns asynchronously a <c>Movie</c> object.</returns>
    /// <exception cref="NoSuchMovieException">Thrown when the specified movie title is "unknown".</exception>
    public Task<Movie> GetMovieInfo(string title)
    {
        // Simulate the behavior when the movie does not exist
        if (title == "unknown") throw new NoSuchMovieException("TestMovie is unknown");
        // Return a mocked Movie object.
        return Task.FromResult(new Movie
        {
            Id = Guid.Empty,
            Title = "MovieFromInfoMock",
            Director = "Mock",
            Duration = "129 min",
            Genre = "Action",
            Description = "I`m mocked",
            Ratings = []
        });
    }
}