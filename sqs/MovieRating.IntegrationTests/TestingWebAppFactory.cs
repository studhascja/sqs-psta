using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MovieRating.Infrastructure;

namespace MovieRating.IntegrationTests;

public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    public TestingWebAppFactory()
    {
        Environment.SetEnvironmentVariable("DB_USER", "test");
        Environment.SetEnvironmentVariable("DB_PASSWORD", "test");
        Environment.SetEnvironmentVariable("DB_SERVER", "test");
        Environment.SetEnvironmentVariable("API_KEY", "test");
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