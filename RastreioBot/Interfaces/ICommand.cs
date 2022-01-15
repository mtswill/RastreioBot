namespace RastreioBot.Interfaces
{
    public interface ICommand
    {
        public int Id { get; }
        public string Description { get; }
        public string CommandMessage { get; }
        Task<(string Result, bool Success, bool Reprocess)> ExecuteAsync(object? obj, IServiceProvider serviceProvider = null!);
    }
}
