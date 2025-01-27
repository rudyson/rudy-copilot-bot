namespace RudyCopilot.Bot.Commands;

public interface ICommandRegistry
{
    ICommandHandler? GetHandler(string command);
    IEnumerable<ICommandHandler> GetAvailableCommandHandlers();
}
