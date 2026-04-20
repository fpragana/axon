using Axon.AI.Interpretation;
using Axon.Core.Commands;

namespace Axon.Application.Tests;

public sealed class MockIntentInterpreterTests
{
    [Fact]
    public async Task AnalyzeAsync_WhenEchoCommandIsProvided_ReturnsEchoIntent()
    {
        var interpreter = new MockIntentInterpreter();

        var result = await interpreter.AnalyzeAsync(UserCommand.Create("echo Olá, AXON"));

        Assert.Equal("system.echo", result.IntentName);
        Assert.Equal("SystemActions", result.PluginName);
        Assert.Equal("echo", result.ActionName);
        Assert.Equal("Olá, AXON", result.Parameters["message"]);
    }
}
