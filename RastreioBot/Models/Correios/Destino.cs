using System.Text.Json.Serialization;

namespace RastreioBot.Models.Correios
{
    public class Destino
    {
        [JsonPropertyName("local")]
        public string Local { get; set; }

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("cidade")]
        public string Cidade { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("uf")]
        public string Uf { get; set; }

        [JsonPropertyName("endereco")]
        public Endereco Endereco { get; set; }
    }
}