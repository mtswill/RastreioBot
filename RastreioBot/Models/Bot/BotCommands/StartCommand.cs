using RastreioBot.Interfaces;

namespace RastreioBot.Models.Bot.BotCommands
{
    internal class StartCommand : ICommand
    {
        public int Id => 0;
        public string Description => "";
        public string CommandMessage => GetDescription();

        public Task<(string Result, bool Success, bool Reprocess)> ExecuteAsync(object? obj, IServiceProvider serviceProvider = null!)
            => Task.FromResult(("", true, false));

        private string GetDescription()
        {
            var commands = new Dictionary<int,string>();

            foreach (var type in GetAllTypesThatImplementICommand())
            {
                var instance = (ICommand)Activator.CreateInstance(type)!;

                if (instance.Id != 0)
                    commands.Add(instance.Id, $"\n{instance.Id} - {instance.Description}");
            }

            var commandsString = string.Empty;

            foreach (var comm in commands.OrderBy(x => x.Key))
                commandsString += comm.Value;

            return "Olá, sou um Bot de rastreamento de encomendas dos Correios.\n\n" +
                "Digite um comando para realizar uma ação:" +
                $"{commandsString}";
        }

        private IEnumerable<Type> GetAllTypesThatImplementICommand()
        {
            return System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => typeof(ICommand).IsAssignableFrom(type) && !type.IsInterface);
        }
    }
}
