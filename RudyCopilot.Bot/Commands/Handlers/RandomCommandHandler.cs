using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RudyCopilot.Bot.Commands.Handlers;
internal class RandomCommandHandler : ICommandHandler
{
    private readonly string[] _availableEmoji = [Emoji.SlotMachine, Emoji.Darts, Emoji.Dice, Emoji.Basketball, Emoji.Bowling, Emoji.Football];
    private readonly string _defaultEmoji = Emoji.Dice;
    private const string _name = "random";
    private const string _description = "Get random dice emoji";
    public string Command => _name;
    public string? Description => _description;

    public async Task<Message> ExecuteAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken = default)
    {
        var randomEmoji = _availableEmoji[Random.Shared.Next(_availableEmoji.Length)] ?? _defaultEmoji;
        var replyParameters = new ReplyParameters { MessageId = message.MessageId };

        var sentMessage = await botClient.SendDice(
            chatId: message.Chat.Id,
            emoji: randomEmoji,
            replyParameters: replyParameters,
            cancellationToken: cancellationToken);

        return sentMessage;
    }
}
