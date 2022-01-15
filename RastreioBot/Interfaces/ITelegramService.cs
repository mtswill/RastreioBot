using RastreioBot.Models.Telegram;

namespace RastreioBot.Interfaces
{
    public interface ITelegramService
    {
        void SetLastMessageId(int messageId);
        Task<GetUpdates> GetUpdatesAsync();
        Task SendMessageAsync(string message);
        Task DeleteMessageAsync(int messageId);
        Task ProcessMessagesAsync(IServiceProvider serviceProvider, GetUpdates updates);
    }
}
