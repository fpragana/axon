using Axon.Application.Abstractions.Execution;
using Axon.Application.Abstractions.Intelligence;
using Axon.Application.Abstractions.Plugins;
using Axon.Core.Commands;
using Axon.Core.Plugins;
using Microsoft.Extensions.Logging;

namespace Axon.Application.Execution;

public sealed class CommandExecutionEngine(
    IIntentInterpreter intentInterpreter,
    IAxonPluginRegistry pluginRegistry,
    ILogger<CommandExecutionEngine> logger) : ICommandExecutionEngine
{
    public async Task<ExecutionResult> ExecuteAsync(UserCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        logger.LogInformation("Processing AXON command received at {ReceivedAtUtc}", command.ReceivedAtUtc);

        var intent = await intentInterpreter.AnalyzeAsync(command, cancellationToken);

        logger.LogInformation(
            "Intent analysis completed with intent {IntentName}, plugin {PluginName}, action {ActionName}, confidence {Confidence}",
            intent.IntentName,
            intent.PluginName,
            intent.ActionName,
            intent.Confidence);

        if (string.IsNullOrWhiteSpace(intent.ActionName))
        {
            return ExecutionResult.Failure(
                "Nenhuma ação de plugin foi identificada para o comando informado.",
                intent.Reasoning ?? "Intent analysis returned no action.");
        }

        var plugin = !string.IsNullOrWhiteSpace(intent.PluginName)
            ? await pluginRegistry.FindByNameAsync(intent.PluginName, cancellationToken)
            : await pluginRegistry.FindByActionAsync(intent.ActionName, cancellationToken);

        if (plugin is null)
        {
            return ExecutionResult.Failure(
                "Nenhum plugin compatível foi encontrado para a ação solicitada.",
                $"Action: {intent.ActionName}");
        }

        var result = await plugin.ExecuteAsync(intent.ActionName, intent.Parameters, cancellationToken);

        logger.LogInformation(
            "Plugin execution finished with status {Succeeded} for plugin {PluginName} and action {ActionName}",
            result.Succeeded,
            plugin.Name,
            intent.ActionName);

        return result;
    }
}
