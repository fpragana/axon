using Axon.Core.Commands;
using Axon.Core.Intelligence;

namespace Axon.Application.Abstractions.Intelligence;

public interface IIntentInterpreter
{
    Task<IntentAnalysis> AnalyzeAsync(UserCommand command, CancellationToken cancellationToken = default);
}
