namespace MovieRating.Core.Interfaces;

/// <summary>
/// Interface <c>IConfigService</c> provides methods to retrieve configuration values for the application.
/// </summary>
public interface IConfigService
{
    /// <summary>
    /// Method <c>GetApiValue</c> retrieves the API-Key for the given environment variable.
    /// </summary>
    /// <param name="environmentNameVariable">The name of the environment variable for the API key.</param>
    /// <returns>Returns the API key as a string.</returns>
    string GetApiValue(string environmentNameVariable);

    /// <summary>
    /// Method <c>GetDbValue</c> retrieves the database setting based on the environment variable name.
    /// </summary>
    /// <param name="environmentNameVariable">The name of the environment variable for the database setting.</param>
    /// <returns>Returns the database setting as a string.</returns>
    string GetDbValue(string environmentNameVariable);
}