using System.Text.Json.Serialization;

namespace RastreioBot.Models.Correios
{
    public class Unidade
    {
        [JsonPropertyName("local")]
        public string Local { get; set; }

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("cidade")]
        public string Cidade { get; set; }

        [JsonPropertyName("uf")]
        public string Uf { get; set; }

        [JsonPropertyName("sto")]
        public string Sto { get; set; }

        [JsonPropertyName("tipounidade")]
        public string Tipounidade { get; set; }

        [JsonPropertyName("endereco")]
        public Endereco Endereco { get; set; }
    }
}