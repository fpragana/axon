using Axon.Core.Plugins;

namespace Axon.Plugins.SystemActions;

public sealed class SystemActionsPlugin : IAxonPlugin
{
    private static readonly IReadOnlyCollection<PluginActionDescriptor> Actions =
    [
        new(
            Name: "echo",
            Description: "Retorna a mesma mensagem recebida, útil para validar o pipeline inicial.",
            Parameters:
            [
                new PluginParameterDescriptor("message", "string", "Mensagem a ser devolvida ao usuário.", true),
            ]),
        new(
            Name: "status",
            Description: "Retorna um status simples do plugin de sistema.",
            Parameters: Array.Empty<PluginParameterDescriptor>()),
    ];

    public string Name => "SystemActions";

    public string Description => "Plugin inicial para validar o pipeline de execução do AXON.";

    public IReadOnlyCollection<PluginActionDescriptor> GetActions() => Actions;

    public Task<ExecutionResult> ExecuteAsync(
        string actionName,
        IReadOnlyDictionary<string, object?> parameters,
        CancellationToken cancellationToken = default)
    {
        return actionName.ToLowerInvariant() switch
        {
            "echo" => Task.FromResult(ExecuteEcho(parameters)),
            "status" => Task.FromResult(ExecutionResult.Success(
                "AXON scaffold ativo e pronto para evolução.",
                new Dictionary<string, object?>
                {
                    ["plugin"] = Name,
                    ["status"] = "ready",
                })),
            _ => Task.FromResult(ExecutionResult.Failure(
                "Ação de plugin não suportada.",
                $"Plugin: {Name}",
                $"Action: {actionName}")),
        };
    }

    private static ExecutionResult ExecuteEcho(IReadOnlyDictionary<string, object?> parameters)
    {
        if (!parameters.TryGetValue("message", out var message) || message is not string text || string.IsNullOrWhiteSpace(text))
        {
            return ExecutionResult.Failure(
                "O parâmetro obrigatório 'message' não foi informado.",
                "Missing parameter: message");
        }

        return ExecutionResult.Success(
            text,
            new Dictionary<string, object?>
            {
                ["plugin"] = "SystemActions",
                ["action"] = "echo",
            });
    }
}
