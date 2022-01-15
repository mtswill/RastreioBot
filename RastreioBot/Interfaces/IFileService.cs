namespace RastreioBot.Interfaces
{
    public interface IFileService
    {
        Task<List<string>> ReadAsync();
        Task WriteAsync(string code);
        Task WriteAsync(List<string> codeList);
        Task RemoveAsync(string code);
    }
}
