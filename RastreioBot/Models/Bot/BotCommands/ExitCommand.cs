using RastreioBot.Interfaces;

namespace RastreioBot.Models.Bot.BotCommands
{
    internal class ExitCommand : ICommand
    {
        public int Id => 0;
        public string Description => "Sair";
        public string CommandMessage => "Você escolheu sair.";

        public Task<(string Result, bool Success, bool Reprocess)> ExecuteAsync(object? obj, IServiceProvider serviceProvider = null!)
            => Task.FromResult(("", true, false));
    }
}
