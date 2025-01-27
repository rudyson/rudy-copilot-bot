using Telegram.Bot;
using Telegram.Bot.Types;

namespace RudyCopilot.Bot.Commands;
public interface ICommandHandler
{
    string Command { get; }
    string? Description { get; }
    Task<Message> ExecuteAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken = default);
}
