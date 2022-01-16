using RastreioBot.Interfaces;
using RastreioBot.Models.Bot.BotCommands;

namespace RastreioBot.Models.Bot
{
    public static class Commands
    {
        public static ICommand GetCommand(this string id)
        {
            if (StartCommandList().Contains(id, StringComparer.InvariantCultureIgnoreCase))
                return new StartCommand();
           
            switch (int.Parse(id))
            {
                case 1: 
                    return new SingleTracking();
                case 2: 
                    return new TrackingAll();
                case 3: 
                    return new InsertTracking();
                case 4: 
                    return new ShowTrackings();
                case 5: 
                    return new RemoveTracking();
                default:
                    return null!;
            }
        }

        public static bool IsExitCommand(this string command)
            => ExitCommandList().Contains(command, StringComparer.InvariantCultureIgnoreCase);

        public static bool IsCommand(this string command)
        {
            var commandList = new List<string>();
            commandList.AddRange(ExecuteCommandList());
            commandList.AddRange(StartCommandList());
            return commandList.Contains(command, StringComparer.InvariantCultureIgnoreCase);
        }
        
        public static bool IsOnlyResponseCommand(this string command)
        {
            var commandList = new List<string>();
            commandList.AddRange(OnlyResponseCommandList());
            return commandList.Contains(command, StringComparer.InvariantCultureIgnoreCase);
        }

        private static List<string> StartCommandList()
        {
            var commandList = new List<string>
            {
                "/start",
                "comandos"
            };

            return commandList;
        }
        
        private static List<string> ExitCommandList()
        {
            var commandList = new List<string>
            {
                "sair"
            };

            return commandList;
        }
        
        private static List<string> ExecuteCommandList()
        {
            var commandList = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5",
            };

            return commandList;
        }
        
        private static List<string> OnlyResponseCommandList()
        {
            var commandList = new List<string>
            {
                "2",
                "4"
            };

            return commandList;
        }
    }
}
