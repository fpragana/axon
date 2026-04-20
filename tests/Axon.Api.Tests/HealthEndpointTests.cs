using Microsoft.AspNetCore.Mvc.Testing;

namespace Axon.Api.Tests;

public sealed class HealthEndpointTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Fact]
    public async Task GetHealth_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/api/health");

        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadAsStringAsync();

        Assert.Contains("\"status\":\"ok\"", payload, StringComparison.OrdinalIgnoreCase);
    }
}
