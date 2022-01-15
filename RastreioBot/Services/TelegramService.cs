using RastreioBot.Interfaces;
using RastreioBot.Models.Telegram;
using System.Text.Json;
using RastreioBot.Models.Bot;
using RastreioBot.Models.Bot.BotCommands;

namespace RastreioBot.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly HttpClient _httpClient;
        private int _lastMessageId = 0;
        private int _chatId = 0;
        private ICommand _waitCommand = null!;

        public TelegramService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetUpdates> GetUpdatesAsync()
        {
            var response = await _httpClient.GetAsync("getUpdates");

            if (!response.IsSuccessStatusCode)
                return null!;

            var content = await response.Content.ReadAsStringAsync();
            var updates = JsonSerializer.Deserialize<GetUpdates>(content)!;
            _chatId = updates!.Result!.Select(x => x!.Message!.Chat!.Id).FirstOrDefault();
            return updates;
        }

        public async Task SendMessageAsync(string message)
        {
            var response = await _httpClient.PostAsync($"sendMessage?chat_id={_chatId}&text={message}", null);

            if (!response.IsSuccessStatusCode)
                return;
        }

        public async Task ProcessMessagesAsync(IServiceProvider serviceProvider, GetUpdates updates)
        {
            if (!updates.Ok)
                return;

            var messages = updates?.Result?.Where(x => x.Message?.MessageId > _lastMessageId).ToList();

            if (messages == null || !messages.Any())
                return;

            var lastId = messages.Select(x => x.Message?.MessageId).LastOrDefault();
            _lastMessageId = (int)lastId!;

            foreach (var message in messages)
            {
                var textMessage = message!.Message!.Text!;

                if (_waitCommand == null)
                {
                    if (textMessage.IsCommand())
                    {
                        ICommand command = textMessage.GetCommand();

                        if (command.GetType() != typeof(StartCommand))
                            _waitCommand = command;

                        if (textMessage.IsOnlyResponseCommand())
                        {
                            _waitCommand = null!;
                            var result = await command.ExecuteAsync(textMessage, serviceProvider);

                            if (!string.IsNullOrEmpty(result.Result))
                                await SendMessageAsync(result.Result);

                            await SendMessageAsync(new StartCommand().CommandMessage);
                        }
                        else
                            await SendMessageAsync(command.CommandMessage);
                    }
                    else
                        await SendMessageAsync("Não consegui interpretar o comando enviado.");
                }
                else
                {
                    if (textMessage.IsExitCommand())
                    {
                        _waitCommand = null!;
                        await SendMessageAsync(new ExitCommand().CommandMessage);
                        await SendMessageAsync(new StartCommand().CommandMessage);
                    }
                    else
                    {
                        var result = await _waitCommand.ExecuteAsync(textMessage, serviceProvider);

                        if (!string.IsNullOrEmpty(result.Result))
                            await SendMessageAsync(result.Result);

                        if (result.Success || !result.Reprocess)
                        {
                            _waitCommand = null!;
                            await SendMessageAsync(new StartCommand().CommandMessage);
                        }
                    }
                }
            }
        }

        public async Task DeleteMessageAsync(int messageId)
        {
            var response = await _httpClient.PostAsync($"deleteMessage?chat_id={_chatId}&message_id={messageId}", null);

            if (!response.IsSuccessStatusCode)
                return;
        }

        public void SetLastMessageId(int messageId)
            => _lastMessageId = messageId;
    }
}
