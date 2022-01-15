using System.Text.Json.Serialization;

namespace RastreioBot.Models.Telegram
{
    public class Entity
    {
        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
