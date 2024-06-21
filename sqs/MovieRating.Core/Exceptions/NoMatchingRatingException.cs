namespace MovieRating.Core.Exceptions;

/// <summary>
/// Class <c>NoMatchingRatingException</c> is a custom Exception if no suitable rating exists
/// </summary>
public class NoMatchingRatingException : Exception
{
    /// <summary>
    /// Method <c>NoMatchingRatingException</c> is a Constructor with message for NoMatchingRatingException
    /// </summary>
    /// <param name="message">custom message of Exception which describes the error</param>
    public NoMatchingRatingException(string message)
        : base(message)
    {
    }
}