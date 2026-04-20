using Axon.AI.DependencyInjection;
using Axon.Application.Abstractions.Execution;
using Axon.Application.Abstractions.Plugins;
using Axon.Application.DependencyInjection;
using Axon.Core.Commands;
using Axon.Core.Plugins;
using Axon.Infrastructure.DependencyInjection;
using Axon.Plugins.HomeAssistant;
using Axon.Plugins.SystemActions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddAxonApplication();
builder.Services.AddAxonAi();
builder.Services.AddAxonInfrastructure();
builder.Services.AddSingleton<IAxonPlugin, SystemActionsPlugin>();
builder.Services.AddSingleton<IAxonPlugin, HomeAssistantPlugin>();

var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/", () => Results.Ok(new
{
    name = "AXON API",
    status = "online",
    endpoints = new[]
    {
        "/api/health",
        "/api/plugins",
        "/api/commands/execute",
    },
}));

app.MapGet("/api/health", () => Results.Ok(new
{
    status = "ok",
    service = "axon-api",
    utc = DateTimeOffset.UtcNow,
}));

app.MapGet("/api/plugins", async (IAxonPluginRegistry pluginRegistry, CancellationToken cancellationToken) =>
{
    var plugins = await pluginRegistry.GetPluginsAsync(cancellationToken);

    var response = plugins.Select(plugin => new
    {
        plugin.Name,
        plugin.Description,
        Actions = plugin.GetActions().Select(action => new
        {
            action.Name,
            action.Description,
            Parameters = action.Parameters.Select(parameter => new
            {
                parameter.Name,
                parameter.Type,
                parameter.Description,
                parameter.IsRequired,
            }),
        }),
    });

    return Results.Ok(response);
});

app.MapPost("/api/commands/execute", async (
    ExecuteCommandRequest request,
    ICommandExecutionEngine executionEngine,
    CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            [nameof(request.Text)] = ["O campo 'text' é obrigatório."],
        });
    }

    var result = await executionEngine.ExecuteAsync(UserCommand.Create(request.Text), cancellationToken);
    return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result);
});

app.Run();

public sealed record ExecuteCommandRequest(string Text);

public partial class Program;
