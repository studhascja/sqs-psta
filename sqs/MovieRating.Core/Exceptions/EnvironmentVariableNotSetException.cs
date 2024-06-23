namespace MovieRating.Core.Exceptions;

/// <summary>
/// Class <c>EnvironmentVariableNotSetException</c> is a custom Exception if the Environment-Variables are not set correctly
/// </summary>
public class EnvironmentVariableNotSetException : Exception
{
    /// <summary>
    /// Method <c>EnvironmentVariableNotSetException</c> is a Constructor with message for EnvironmentVariableNotSetException
    /// </summary>
    /// <param name="message">custom message of Exception which describes the error</param>
    public EnvironmentVariableNotSetException(string message)
        : base(message)
    {
    }
}