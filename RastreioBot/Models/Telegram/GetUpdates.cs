using System.Text.Json.Serialization;

namespace RastreioBot.Models.Telegram
{
    public class GetUpdates
    {
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("result")]
        public List<TelegramResult>? Result { get; set; }
    }
}
