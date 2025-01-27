﻿using Microsoft.Extensions.Hosting;

namespace RudyCopilot.Bot;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(Startup.ConfigureAppConfiguration)
            .ConfigureServices(Startup.ConfigureServices);
    }
}