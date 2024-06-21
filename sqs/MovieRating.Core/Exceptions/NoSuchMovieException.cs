namespace MovieRating.Core.Exceptions;

/// <summary>
/// Class <c>NoSuchMovieException</c> is a custom Exception if no suitable movie exists
/// </summary>
public class NoSuchMovieException : Exception
{
    /// <summary>
    /// Method <c>NoSuchMovieException</c> is a Constructor with message for NoSuchMovieException
    /// </summary>
    /// <param name="message">custom message of Exception which describes the error</param>
    public NoSuchMovieException(string message)
        : base(message)
    {
    }
}