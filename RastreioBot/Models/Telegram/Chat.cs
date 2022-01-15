using System.Text.Json.Serialization;

namespace RastreioBot.Models.Telegram
{
    public class Chat
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
