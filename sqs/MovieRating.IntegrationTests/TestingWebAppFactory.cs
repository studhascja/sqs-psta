using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MovieRating.Infrastructure;
using MovieRating.Infrastructure.Services;

namespace MovieRating.IntegrationTests;

public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    private const string EnvironmentNameApiKey = "API_KEY";
    private readonly ConfigService _configService = new();
    public TestingWebAppFactory()
    {
        Environment.SetEnvironmentVariable("DB_USER", "test");
        Environment.SetEnvironmentVariable("DB_PASSWORD", "test");
        Environment.SetEnvironmentVariable("DB_SERVER", "test");
        Environment.SetEnvironmentVariable("API_KEY", _configService.GetApiValue(EnvironmentNameApiKey));
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove database service
            var dbContextDescriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<MovieContext>));

            services.Remove(dbContextDescriptor);

            // Add database service and use in memory database
            services.AddDbContext<MovieContext>(options =>
            {
                options.UseInMemoryDatabase(nameof(TestingWebAppFactory));
            });
        });
    }
}