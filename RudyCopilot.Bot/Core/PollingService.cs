using Microsoft.Extensions.Logging;
using RudyCopilot.Bot.Abstract;

namespace RudyCopilot.Bot.Core;
public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
    : PollingServiceBase<ReceiverService>(serviceProvider, logger);