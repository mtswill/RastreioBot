using Microsoft.Extensions.DependencyInjection;
using RastreioBot.Interfaces;
using System.Text.Json;

namespace RastreioBot.Services
{
    public class BotService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly int _telegramWaitMs;
        private readonly int _correioWaitMs;
        private readonly int _lastMessageId;

        public BotService(IServiceProvider serviceProvider, int telegramWaitMs, int correioWaitMs, int lastMessageId)
        {
            _serviceProvider = serviceProvider;
            _telegramWaitMs = telegramWaitMs;
            _correioWaitMs = correioWaitMs;
            _lastMessageId = lastMessageId;
        }

        public Task ExecuteBotAsync()
        {
            var cts = new CancellationTokenSource();
            Task.Factory.StartNew(() => ExecuteTelegramAsync(cts.Token));
            Task.Factory.StartNew(() => ExecuteCorreiosAsync(cts.Token));
            return Task.CompletedTask;
        }

        private async Task ExecuteTelegramAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var telegramService = scope.ServiceProvider.GetRequiredService<ITelegramService>();

                if (_lastMessageId > 0)
                    telegramService.SetLastMessageId(_lastMessageId);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var updates = await telegramService.GetUpdatesAsync();

                    if (updates != null)
                        await telegramService.ProcessMessagesAsync(_serviceProvider, updates);

                    await Task.Delay(_telegramWaitMs);
                }
            }
        }
        
        private async Task ExecuteCorreiosAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var correioService = scope.ServiceProvider.GetRequiredService<ICorreioService>();
                var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    var trackingList = await fileService.ReadAsync();
                    var trackings = await correioService.GetTrackingsAsync(trackingList);

                    await Task.Delay(_correioWaitMs);
                }
            }
        }
    }
}
