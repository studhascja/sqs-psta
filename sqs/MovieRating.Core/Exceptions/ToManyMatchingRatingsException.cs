namespace MovieRating.Core.Exceptions;

/// <summary>
/// Class <c>ToManyMatchingRatingsException</c> is a custom Exception if more than one matching rating was found
/// </summary>
public class ToManyMatchingRatingsException : Exception
{
    /// <summary>
    /// Method <c>ToManyMatchingRatingsException</c> is a Constructor with message for NoSuchMovieException
    /// </summary>
    /// <param name="message">custom message of Exception which describes the error</param>
    public ToManyMatchingRatingsException(string message)
        : base(message)
    {
    }
}