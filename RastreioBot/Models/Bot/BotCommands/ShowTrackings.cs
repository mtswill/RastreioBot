using Microsoft.Extensions.DependencyInjection;
using RastreioBot.Interfaces;

namespace RastreioBot.Models.Bot.BotCommands
{
    public class ShowTrackings : ICommand
    {
        public int Id => 4;
        public string Description => "Visualizar códigos salvos";
        public string CommandMessage => "Visualizando todos os rastreamentos salvos.";

        public async Task<(string Result, bool Success, bool Reprocess)> ExecuteAsync(object? obj, IServiceProvider serviceProvider = null!)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();
                var trackings = await fileService.ReadAsync();

                if (trackings == null || !trackings.Any())
                    return ("Você não possui rastreamentos salvos.", false, false);

                var returnMessage = "Códigos salvos:\n";
                trackings.ForEach(tracking => returnMessage += $"\n{tracking}");

                return (returnMessage, true, false);
            }
        }
    }
}
