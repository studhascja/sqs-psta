using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;

namespace MovieRating.Infrastructure.Services;

/// <summary>
/// Class <c>ConfigService</c> provides methods to retrieve configuration values for the application.
/// </summary>
public class ConfigService : IConfigService
{
    /// <summary>
    /// Method <c>GetApiValue</c> retrieves the API key based on the environment variable name.
    /// </summary>
    /// <param name="environmentNameVariable">The name of the environment variable for the API key.</param>
    /// <returns>Returns the API key as a string.</returns>
    /// <exception cref="EnvironmentVariableNotSetException">Thrown when the environment variable is not set.</exception>
    public string GetApiValue(string environmentNameVariable)
    {
        return Environment.GetEnvironmentVariable(environmentNameVariable) ??
               throw new EnvironmentVariableNotSetException("API-Key not set correctly");
    }

    /// <summary>
    /// Method <c>GetDbValue</c> retrieves the database setting based on the environment variable name.
    /// </summary>
    /// <param name="environmentNameVariable">The name of the environment variable for the database setting.</param>
    /// <returns>Returns the database setting as a string.</returns>
    /// <exception cref="EnvironmentVariableNotSetException">Thrown when the environment variable is not set.</exception>
    public string GetDbValue(string environmentNameVariable)
    {
        return Environment.GetEnvironmentVariable(environmentNameVariable) ??
               throw new EnvironmentVariableNotSetException("DB-Environment-Variables not set correctly");
    }
}