namespace MovieRating.Core.Exceptions;

public class NoSuchMovieException : Exception
{
    public NoSuchMovieException(string message)
        : base(message)
    {
    }
}