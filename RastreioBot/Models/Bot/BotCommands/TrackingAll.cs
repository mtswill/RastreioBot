using Microsoft.Extensions.DependencyInjection;
using RastreioBot.Helpers;
using RastreioBot.Interfaces;

namespace RastreioBot.Models.Bot.BotCommands
{
    public class TrackingAll : ICommand
    {
        public int Id => 2;
        public string Description => "Buscar todos os rastreamentos salvos.";
        public string CommandMessage => "Buscando todos os rastreamentos salvos.";

        public async Task<(string Result, bool Success, bool Reprocess)> ExecuteAsync(object? obj, IServiceProvider serviceProvider = null!)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();

                var trackings = await fileService.ReadAsync();

                if (trackings == null || !trackings.Any())
                    return ("Você não possui rastreamentos salvos.", false, false);

                var correioService = scope.ServiceProvider.GetRequiredService<ICorreioService>();
                var result = await correioService.GetTrackingsAsync(trackings);

                if (result == null)
                    return ("Não foi possível rastrear. Tente novamente mais tarde.", false, false);

                var formatResponse = result.ToResponse();

                if (formatResponse.IsSuccess)
                    return (formatResponse.Result, true, true);

                return (formatResponse.Result, false, true);
            }
        }
    }
}
