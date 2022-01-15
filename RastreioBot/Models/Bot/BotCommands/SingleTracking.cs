using Microsoft.Extensions.DependencyInjection;
using RastreioBot.Helpers;
using RastreioBot.Interfaces;

namespace RastreioBot.Models.Bot.BotCommands
{
    public class SingleTracking : ICommand
    {
        public int Id => 1;
        public string Description => "Rastreamento avulso";
        public string CommandMessage => "Insira o código de rastreio para buscar as informações da encomenda:";

        public async Task<(string Result, bool Success, bool Reprocess)> ExecuteAsync(object? obj, IServiceProvider serviceProvider = null!)
        {
            var tracking = obj!.ToString();

            if (string.IsNullOrEmpty(tracking) || tracking.Length != 13)
                return ("Código de rastreio inválido! Digite o código novamente ou digite 'sair' para não rastrear.", false, true);

            using (var scope = serviceProvider.CreateScope())
            {
                var correioService = scope.ServiceProvider.GetRequiredService<ICorreioService>();
                var result = await correioService.GetTrackingAsync(tracking);

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
