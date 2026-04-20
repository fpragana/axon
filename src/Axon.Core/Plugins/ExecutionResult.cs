namespace Axon.Core.Plugins;

public sealed record ExecutionResult(
    bool Succeeded,
    string Message,
    IReadOnlyDictionary<string, object?> Metadata,
    IReadOnlyCollection<string> Errors)
{
    public static ExecutionResult Success(string message, IReadOnlyDictionary<string, object?>? metadata = null) =>
        new(true, message, metadata ?? new Dictionary<string, object?>(), Array.Empty<string>());

    public static ExecutionResult Failure(string message, params string[] errors) =>
        new(false, message, new Dictionary<string, object?>(), errors);
}
