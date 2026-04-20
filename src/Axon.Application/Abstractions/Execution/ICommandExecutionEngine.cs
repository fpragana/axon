using Axon.Core.Commands;
using Axon.Core.Plugins;

namespace Axon.Application.Abstractions.Execution;

public interface ICommandExecutionEngine
{
    Task<ExecutionResult> ExecuteAsync(UserCommand command, CancellationToken cancellationToken = default);
}
