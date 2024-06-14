namespace MovieRating.Core.Exceptions;

public class NoMatchingRatingException : Exception
{
    public NoMatchingRatingException()
    {
        
    }
    
    public NoMatchingRatingException(string message)
        : base(message)
    {
    }
}