using RastreioBot.Interfaces;
using RastreioBot.Models.Correios;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RastreioBot.Services
{
    public class CorreioService : ICorreioService
    {
        private readonly HttpClient _httpClient;

        public CorreioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CorreiosResponse> GetTrackingAsync(string tracking)
            => await GetTrackingsAsync(new List<string> { tracking });

        public async Task<CorreiosResponse> GetTrackingsAsync(List<string> trackings)
        {
            var content = BuildStringContent(trackings);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            var response = await _httpClient.PostAsync("", content);

            if (!response.IsSuccessStatusCode)
                return null!;

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CorreiosResponse>(responseContent)!;
        }

        private StringContent BuildStringContent(List<string> trackings)
        {
            var trackingString = string.Empty;
            trackings.ForEach(tracking => trackingString += tracking);
            var xml = XmlRequestModel.XmlModel.Replace("@tracking_code_list", trackingString);
            return new StringContent(xml);
        }
    }
}
