using RastreioBot.Models.Correios;

namespace RastreioBot.Interfaces
{
    public interface ICorreioService
    {
        Task<CorreiosResponse> GetTrackingAsync(string tracking);
        Task<CorreiosResponse> GetTrackingsAsync(List<string> trackings);
    }
}
