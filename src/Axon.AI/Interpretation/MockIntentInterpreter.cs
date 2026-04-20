using Axon.Application.Abstractions.Intelligence;
using Axon.Core.Commands;
using Axon.Core.Intelligence;

namespace Axon.AI.Interpretation;

public sealed class MockIntentInterpreter : IIntentInterpreter
{
    public Task<IntentAnalysis> AnalyzeAsync(UserCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var normalized = command.Text.Trim();
        if (string.IsNullOrWhiteSpace(normalized))
        {
            return Task.FromResult(IntentAnalysis.Unknown("Empty command."));
        }

        if (normalized.StartsWith("echo ", StringComparison.OrdinalIgnoreCase))
        {
            var message = normalized[5..].Trim();

            return Task.FromResult(new IntentAnalysis(
                IntentName: "system.echo",
                PluginName: "SystemActions",
                ActionName: "echo",
                Confidence: 0.98d,
                Parameters: new Dictionary<string, object?>
                {
                    ["message"] = message,
                },
                Reasoning: "Mock interpreter matched the 'echo' command."));
        }

        if (normalized.Contains("status", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(new IntentAnalysis(
                IntentName: "system.status",
                PluginName: "SystemActions",
                ActionName: "status",
                Confidence: 0.82d,
                Parameters: new Dictionary<string, object?>(),
                Reasoning: "Mock interpreter matched a status inquiry."));
        }

        return Task.FromResult(IntentAnalysis.Unknown("Mock interpreter did not find a compatible intent."));
    }
}
