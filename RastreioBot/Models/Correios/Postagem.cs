using System.Text.Json.Serialization;

namespace RastreioBot.Models.Correios
{
    public class Postagem
    {
        [JsonPropertyName("cepdestino")]
        public string Cepdestino { get; set; }

        [JsonPropertyName("ar")]
        public string Ar { get; set; }

        [JsonPropertyName("mp")]
        public string Mp { get; set; }

        [JsonPropertyName("dh")]
        public string Dh { get; set; }

        [JsonPropertyName("peso")]
        public string Peso { get; set; }

        [JsonPropertyName("volume")]
        public string Volume { get; set; }

        [JsonPropertyName("dataprogramada")]
        public string Dataprogramada { get; set; }

        [JsonPropertyName("datapostagem")]
        public string Datapostagem { get; set; }

        [JsonPropertyName("prazotratamento")]
        public string Prazotratamento { get; set; }
    }
}