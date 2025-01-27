using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RudyCopilot.Bot.Abstract;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace RudyCopilot.Bot.Core;
public class ReceiverService(ITelegramBotClient botClient, UpdateHandler updateHandler, ILogger<ReceiverServiceBase<UpdateHandler>> logger, IOptions<ReceiverOptions> receiverOptions)
    : ReceiverServiceBase<UpdateHandler>(botClient, updateHandler, logger, receiverOptions);