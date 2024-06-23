using NBomber.Contracts;
using NBomber.CSharp;


namespace MovieRating.LoadTests;

/// <summary>
/// Load tests for API-Controller.
/// </summary>
public class LoadTest : IClassFixture<TestingWebAppFactory>
{
    private readonly TestingWebAppFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <c>LoadTest</c> class with the specified TestingWebAppFactory factory.
    /// </summary>
    /// <param name="factory">An instance of <c>TestingWebAppFactory</c> to create a Client with which the Program can be tested.</param>
    public LoadTest(TestingWebAppFactory factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Load tests for API-Controller Get Movie.
    /// </summary>
    [Fact]
    public void InjectMovie()
    {
        // Prep
        var client = _factory.CreateClient();
        var scenarioMovie = Scenario.Create("Movie_Workload", async _ =>
            {
                var response = await client.GetAsync("/api/v1/Movie/Godzilla");
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            })
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.Inject(rate: 70, // 70 Requests per second
                    interval: TimeSpan.FromSeconds(1), // for 60 seconds
                    during: TimeSpan.FromSeconds(60))
            );

        // Act
        var result = NBomberRunner
            .RegisterScenarios(scenarioMovie)
            .Run();

        var movieStats = result.ScenarioStats.Get("Movie_Workload");

        // Assert
        Assert.True(movieStats.Ok.Latency.MeanMs < 500); // Mean Latency has to be under 500 ms
        Assert.Equal(0, movieStats.Fail.Request.Count); // No Request may fail
    }

    /// <summary>
    /// Load tests for API-Controller Get All-Movies.
    /// </summary>
    [Fact]
    public async Task InjectMovieList()
    {
        // Prep
        var client = _factory.CreateClient();

        await client.GetAsync("api/v1/Movie/Godzilla");

        var scenarioList = Scenario.Create("Movie_List", async _ =>
            {
                var response = await client.GetAsync("api/v1/Movie");
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            })
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.Inject(rate: 70, // 70 Requests per second
                    interval: TimeSpan.FromSeconds(1), // for 60 seconds
                    during: TimeSpan.FromSeconds(60))
            );

        // Act
        var result = NBomberRunner
            .RegisterScenarios(scenarioList)
            .Run();

        var movieListStats = result.ScenarioStats.Get("Movie_List");

        // Assert
        Assert.True(movieListStats.Ok.Latency.MeanMs < 500); // Mean Latency has to be under 500 ms
        Assert.Equal(0, movieListStats.Fail.Request.Count); // No Request may fail
    }
}