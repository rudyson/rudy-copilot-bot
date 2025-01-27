using Telegram.Bot;
using Telegram.Bot.Types;

namespace RudyCopilot.Bot.Commands.Handlers;
internal class HelpCommandHandler : ICommandHandler
{
    private const string _name = "help";
    private const string _description = "List of available commands";
    public string Command => _name;
    public string? Description => _description;

    public async Task<Message> ExecuteAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        // TODO: Fix circular dependency
        //    var replyParameters = new ReplyParameters { MessageId = message.MessageId };
        //    var helpStringBuilder = new StringBuilder("<b><u>Bot menu</u></b>:");
        //IEnumerable<ICommandHandler> _availableCommands = commandRegistry.GetAvailableCommandHandlers();
        //    foreach (var commandHandler in _availableCommands)
        //    {
        //        helpStringBuilder.Append('/');
        //        helpStringBuilder.Append(commandHandler.Command.PadLeft(12));
        //        if (!string.IsNullOrEmpty(commandHandler.Description))
        //        {
        //            helpStringBuilder.Append($" - {commandHandler.Description}");
        //        }
        //        helpStringBuilder.AppendLine();
        //    }

        //    var sentMessage = await botClient.SendMessage(
        //        chatId: message.Chat.Id,
        //        text: helpStringBuilder.ToString(),
        //        replyParameters: replyParameters,
        //        parseMode: ParseMode.Html,
        //        replyMarkup: new ReplyKeyboardRemove(),
        //        cancellationToken: cancellationToken);

        //    return sentMessage;
    }
}
