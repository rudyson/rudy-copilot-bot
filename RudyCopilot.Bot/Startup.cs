using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RudyCopilot.Bot.Commands;
using RudyCopilot.Bot.Commands.Handlers;
using RudyCopilot.Bot.Core;
using RudyCopilot.Bot.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace RudyCopilot.Bot;
internal static class Startup
{
    public static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder config)
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                      .AddUserSecrets<Program>()
                      .AddEnvironmentVariables();
    }
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddOptions<TelegramBotOptions>().BindConfiguration(TelegramBotOptions.SectionName);
        services.AddOptions<ReceiverOptions>().Configure(options =>
        {
            options.DropPendingUpdates = true;
            options.AllowedUpdates = Array.Empty<UpdateType>();
        });
        services.AddSingleton<ICommandRegistry, CommandRegistry>();
        RegisterCommandHandlers(services);

        // Register named HttpClient to benefits from IHttpClientFactory and consume it with ITelegramBotClient typed client.
        // See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients
        // and https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
        services.AddHttpClient(nameof(TelegramBotClient)).RemoveAllLoggers()
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    TelegramBotOptions? botConfiguration = sp.GetService<IOptions<TelegramBotOptions>>()?.Value;
                    ArgumentNullException.ThrowIfNull(botConfiguration);
                    TelegramBotClientOptions options = new(botConfiguration.BotToken);
                    return new TelegramBotClient(options, httpClient);
                });

        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });
    }

    private static void RegisterCommandHandlers(IServiceCollection services)
    {
        services.AddTransient<ICommandHandler, RandomCommandHandler>();
        //services.AddTransient<ICommandHandler, HelpCommandHandler>();
        //services.AddTransient<ICommandHandler, StartCommandHandler>();
        //services.AddTransient<ICommandHandler, NewsCommandHandler>();
        //services.AddTransient<ICommandHandler, CurrencyCommandHandler>();
        //services.AddTransient<ICommandHandler, AirraidmapCommandHandler>();
    }
}
