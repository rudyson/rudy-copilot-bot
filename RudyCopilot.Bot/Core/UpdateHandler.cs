using Microsoft.Extensions.Logging;
using RudyCopilot.Bot.Commands;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RudyCopilot.Bot.Core;
public class UpdateHandler(
    ITelegramBotClient bot,
    ILogger<UpdateHandler> logger,
    ICommandRegistry commandRegistry
    ) : IUpdateHandler
{
    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        logger.LogInformation("HandleError: {Exception}", exception);
        // Cooldown in case of network connection error
        if (exception is RequestException)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await (update switch
        {
            { Message: { } message } => OnMessage(botClient, message, cancellationToken),
            { EditedMessage: { } message } => OnMessage(botClient, message, cancellationToken),
            _ => Task.CompletedTask
        });
    }

    private async Task OnMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken = default)
    {
        if (
            message.Text is not { }
            || message is not { Type: MessageType.Text }
            || message.Entities is not { }
        )
        {
            return;
        }

        var messageEntityBotCommand = message.Entities.FirstOrDefault(x => x is { Type: MessageEntityType.BotCommand });

        if (messageEntityBotCommand is null)
        {
            return;
        }

        // TODO: Recognition of current bot invoked
        // TODO: Command parameters
        // TODO: Ignore if not called manualy from zero offset

        var commandInvoked = message.Text[messageEntityBotCommand.Offset..messageEntityBotCommand.Length];
        commandInvoked = commandInvoked.Split(' ')[0];
        commandInvoked = commandInvoked.Split('@')[0];
        commandInvoked = commandInvoked.Split('/')[1];
        var commandHandler = commandRegistry.GetHandler(commandInvoked);

        if (commandHandler is null)
        {
            return;
        }

        await commandHandler.ExecuteAsync(botClient, message, cancellationToken);
    }
}