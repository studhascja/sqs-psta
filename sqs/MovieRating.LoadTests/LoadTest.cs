using NBomber.Contracts;
using NBomber.CSharp;


namespace MovieRating.LoadTests;

public class LoadTest : IClassFixture<TestingWebAppFactory>
{
    private readonly TestingWebAppFactory _factory;

    public LoadTest(TestingWebAppFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public void InjectMovie()
    {
        // Prep
        var client = _factory.CreateClient();
        var scenarioMovie = Scenario.Create("Movie_Workload", async _ =>
            {
                var response = await client.GetAsync("/api/v1/Movie/Godzilla");
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
            })
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.Inject(rate: 5,
                    interval: TimeSpan.FromSeconds(1),
                    during: TimeSpan.FromSeconds(2))
            );

        // Act
        var result = NBomberRunner
            .RegisterScenarios(scenarioMovie)
            .Run();

        var movieStats = result.ScenarioStats.Get("Movie_Workload");

        // Assert
        Assert.True(movieStats.Ok.Latency.MeanMs < 500);
        Assert.Equal(0, movieStats.Fail.Request.Count);
    }

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
                Simulation.Inject(rate: 5,
                    interval: TimeSpan.FromSeconds(1),
                    during: TimeSpan.FromSeconds(2))
            );

        // Act
        var result = NBomberRunner
            .RegisterScenarios(scenarioList)
            .Run();

        var movieListStats = result.ScenarioStats.Get("Movie_List");

        // Assert
        Assert.True(movieListStats.Ok.Latency.MeanMs < 500);
        Assert.Equal(0, movieListStats.Fail.Request.Count);
    }
}