using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RudyCopilot.Bot.Abstract;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace RudyCopilot.Bot.Core.Example;
public class ExampleReceiverService(ITelegramBotClient botClient, ExampleUpdateHandler updateHandler, ILogger<ReceiverServiceBase<ExampleUpdateHandler>> logger, IOptions<ReceiverOptions> receiverOptions)
    : ReceiverServiceBase<ExampleUpdateHandler>(botClient, updateHandler, logger, receiverOptions);