using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RastreioBot.Interfaces;
using RastreioBot.Services;

IConfiguration configuration = new ConfigurationBuilder()
                                     .AddJsonFile("appsettings.json")
                                     .Build();

var accessToken = configuration.GetSection("TelegramAccessToken").Value;
var correiosUri = configuration.GetSection("CorreiosUri").Value;
var telegramUri = configuration.GetSection("TelegramUri").Value;
var telegramWaitMs = configuration.GetSection("TelegramWaitMs").Value;
var correiosWaitMs = configuration.GetSection("CorreiosWaitMs").Value;
var lastMessageId = configuration.GetSection("LastMessageId").Value;

IServiceCollection services = new ServiceCollection();

services.AddScoped<ICorreioService, CorreioService>();
services.AddScoped<ITelegramService, TelegramService>();
services.AddScoped<IFileService, FileService>();

services.AddHttpClient<ITelegramService, TelegramService>(x =>
{
    x.BaseAddress = new Uri($"{telegramUri}{accessToken}/");
});

services.AddHttpClient<ICorreioService, CorreioService>(x =>
{
    x.BaseAddress = new Uri(correiosUri);
});

IServiceProvider serviceProvider = services.BuildServiceProvider();

try
{
    var bot = new BotService(serviceProvider, int.Parse(telegramWaitMs), int.Parse(correiosWaitMs), int.Parse(lastMessageId));
    await bot.ExecuteBotAsync();

    Console.WriteLine("Bot em execução . . .");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine("\n\n" + ex.Message);
}