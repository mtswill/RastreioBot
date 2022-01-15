using System.Text.Json.Serialization;

namespace RastreioBot.Models.Telegram
{
    public class TelegramResult
    {
        [JsonPropertyName("update_id")]
        public int UpdateId { get; set; }

        [JsonPropertyName("message")]
        public Message? Message { get; set; }
    }
}
