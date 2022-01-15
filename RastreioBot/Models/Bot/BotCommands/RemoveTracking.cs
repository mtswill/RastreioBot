using Microsoft.Extensions.DependencyInjection;
using RastreioBot.Interfaces;

namespace RastreioBot.Models.Bot.BotCommands
{
    public class RemoveTracking : ICommand
    {
        public int Id => 5;
        public string Description => "Remover código de rastreio";
        public string CommandMessage => "Insira o código de rastreio a ser removido:";

        public async Task<(string Result, bool Success, bool Reprocess)> ExecuteAsync(object? obj, IServiceProvider serviceProvider = null!)
        {
            var tracking = obj!.ToString();

            if (string.IsNullOrEmpty(tracking) || tracking.Length != 13)
                return ("Código de rastreio inválido! Digite o código novamente ou digite 'sair' para não remover.", false, true);

            using (var scope = serviceProvider.CreateScope())
            {
                var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();
                var trackings = await fileService.ReadAsync();

                if (trackings == null || !trackings.Any())
                    return ("Você não possui rastreamentos salvos.", false, false);

                if (!tracking.Contains(tracking))
                    return ("Código não encontrado.", false, true);

                await fileService.RemoveAsync(tracking);
                return ("Código removido com sucesso!", true, false);
            }
        }
    }
}
