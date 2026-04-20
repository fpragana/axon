namespace Axon.Core.Intelligence;

public sealed record IntentAnalysis(
    string IntentName,
    string? PluginName,
    string? ActionName,
    double Confidence,
    IReadOnlyDictionary<string, object?> Parameters,
    string? Reasoning = null)
{
    public static IntentAnalysis Unknown(string? reasoning = null) =>
        new(
            IntentName: "unknown",
            PluginName: null,
            ActionName: null,
            Confidence: 0,
            Parameters: new Dictionary<string, object?>(),
            Reasoning: reasoning);
}
