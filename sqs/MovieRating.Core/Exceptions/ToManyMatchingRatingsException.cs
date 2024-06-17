namespace MovieRating.Core.Exceptions;

public class ToManyMatchingRatingsException : Exception
{
    public ToManyMatchingRatingsException(string message)
        : base(message)
    {
    }
}