using RastreioBot.Interfaces;

namespace RastreioBot.Services
{
    public class FileService : IFileService
    {
        private readonly string _filePath = "Files\\tracking_codes.txt";

        public async Task<List<string>> ReadAsync()
        {
            var trackingList = new List<string>();

            using(var reader = new StreamReader(_filePath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                    trackingList.Add(line);
            }

            return trackingList;
        }

        public async Task RemoveAsync(string code)
        {
            var read = await File.ReadAllLinesAsync(_filePath);
            var lines = read.Where(line => line.Trim() != code).ToArray();
            await File.WriteAllLinesAsync(_filePath, lines);
        }

        public Task WriteAsync(string code)
            => Task.Factory.StartNew(() => WriteFileAsync(code));

        public Task WriteAsync(List<string> codeList)
        {
            codeList.ForEach(x => Task.Factory.StartNew(() => WriteFileAsync(x)));
            return Task.CompletedTask;
        }

        private async Task WriteFileAsync(string code)
            => await File.AppendAllTextAsync(_filePath, code + Environment.NewLine);
    }
}
