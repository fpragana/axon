namespace Axon.Core.Commands;

public sealed record UserCommand(string Text, DateTimeOffset ReceivedAtUtc)
{
    public static UserCommand Create(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);
        return new UserCommand(text.Trim(), DateTimeOffset.UtcNow);
    }
}
