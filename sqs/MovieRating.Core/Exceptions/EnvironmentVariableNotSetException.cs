namespace MovieRating.Core.Exceptions;

public class EnvironmentVariableNotSetException : Exception
{
    public EnvironmentVariableNotSetException()
    {
        
    }
    
    public EnvironmentVariableNotSetException(string message)
        : base(message)
    {
        
    }
}