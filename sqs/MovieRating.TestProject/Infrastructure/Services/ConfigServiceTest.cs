using MovieRating.Core.Exceptions;
using MovieRating.Infrastructure.Services;

namespace MovieRating.TestProject.Infrastructure.Services;

public class ConfigServiceTest
{
    [Fact]
    public void GetEnvironmentVariable()
    {
        // Prep
        Environment.SetEnvironmentVariable("API_TEST", "api");
        Environment.SetEnvironmentVariable("DB_TEST", "db");

        var configService = new ConfigService();
        
        // Act
        var apiResult = configService.GetApiValue("API_TEST");
        var dbResult = configService.GetDbValue("DB_TEST");
        
        //Assert
        Assert.Equal("api", apiResult);
        Assert.Equal("db", dbResult);
    }
    
    [Fact]
    public void GetEnvironmentVariableException()
    {
        // Prep
        Environment.SetEnvironmentVariable("API_TEST", null);
        Environment.SetEnvironmentVariable("DB_TEST", null);

        var configService = new ConfigService();
        
        //Assert
        Assert.Throws<EnvironmentVariableNotSetException>(() => configService.GetApiValue("API_TEST"));
        Assert.Throws<EnvironmentVariableNotSetException>(() => configService.GetDbValue("DB_TEST"));
    }
}