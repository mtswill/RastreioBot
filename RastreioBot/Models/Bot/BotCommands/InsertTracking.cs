using Microsoft.Extensions.DependencyInjection;
using RastreioBot.Interfaces;

namespace RastreioBot.Models.Bot.BotCommands
{
    public class InsertTracking : ICommand
    {
        public int Id => 3;
        public string Description => "Salvar código de rastreio";
        public string CommandMessage => "Insira o código de rastreio para salvá-lo:";

        public async Task<(string Result, bool Success, bool Reprocess)> ExecuteAsync(object? obj, IServiceProvider serviceProvider = null!)
        {
            var tracking = obj!.ToString();

            if (string.IsNullOrEmpty(tracking) || tracking.Length != 13)
                return ("Código de rastreio inválido! Digite o código novamente ou digite 'sair' para não salvar.", false, true);

            using (var scope = serviceProvider.CreateScope())
            {
                var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();
                await fileService.WriteAsync(tracking);

                return ("Código salvo com sucesso!", true, true);
            }
        }
    }
}
