using MovieRating.Core.Exceptions;
using MovieRating.Core.Interfaces;

namespace MovieRating.Infrastructure.Services;

public class ConfigService : IConfigService
{
    public string GetApiValue(string environmentNameVariable)
    {
        return Environment.GetEnvironmentVariable(environmentNameVariable) ?? throw new EnvironmentVariableNotSetException("API-Key not set correctly");
    }

    public string GetDbValue(string environmentNameVariable)
    {
        return Environment.GetEnvironmentVariable(environmentNameVariable) ?? throw new EnvironmentVariableNotSetException("DB-Environment-Variables not set correctly");
    }
}