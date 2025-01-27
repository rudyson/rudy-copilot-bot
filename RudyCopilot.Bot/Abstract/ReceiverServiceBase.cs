using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace RudyCopilot.Bot.Abstract;

/// <summary>An abstract class to compose Receiver Service and Update Handler classes</summary>
/// <typeparam name="TUpdateHandler">Update Handler to use in Update Receiver</typeparam>
public abstract class ReceiverServiceBase<TUpdateHandler>(ITelegramBotClient botClient, TUpdateHandler updateHandler, ILogger<ReceiverServiceBase<TUpdateHandler>> logger, IOptions<ReceiverOptions> receiverOptions)
    : IReceiverService where TUpdateHandler : IUpdateHandler
{
    /// <summary>Start to service Updates with provided Update Handler class</summary>
    public async Task ReceiveAsync(CancellationToken stoppingToken)
    {
        var me = await botClient.GetMe(stoppingToken);
        logger.LogInformation("Start receiving updates for {BotName} (Id: {Id})", me.Username ?? "Unknown", me.Id);

        // Start receiving updates
        await botClient.ReceiveAsync(updateHandler, receiverOptions.Value, stoppingToken);
    }
}