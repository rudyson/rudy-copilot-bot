namespace RudyCopilot.Bot.Options;
public class TelegramBotOptions
{
    public const string SectionName = "Telegram";
    public required string BotToken { get; init; } = default!;
}
