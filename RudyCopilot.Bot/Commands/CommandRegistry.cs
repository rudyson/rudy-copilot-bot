namespace RudyCopilot.Bot.Commands;

internal class CommandRegistry : ICommandRegistry
{
    private readonly Dictionary<string, ICommandHandler> _registeredHandlers;

    public CommandRegistry(IEnumerable<ICommandHandler> handlers)
    {
        _registeredHandlers = handlers.ToDictionary(h => h.Command, StringComparer.OrdinalIgnoreCase);
    }

    public IEnumerable<ICommandHandler> GetAvailableCommandHandlers()
    {
        return _registeredHandlers.Values;
    }

    public ICommandHandler? GetHandler(string command) => _registeredHandlers.TryGetValue(command, out var handler) ? handler : null;
}
