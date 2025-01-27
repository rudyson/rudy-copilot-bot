using Microsoft.Extensions.Logging;
using RudyCopilot.Bot.Abstract;

namespace RudyCopilot.Bot.Core.Example;
public class ExamplePollingService(IServiceProvider serviceProvider, ILogger<ExamplePollingService> logger)
    : PollingServiceBase<ExampleReceiverService>(serviceProvider, logger);