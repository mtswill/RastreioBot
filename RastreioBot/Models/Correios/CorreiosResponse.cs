using System.Text.Json.Serialization;

namespace RastreioBot.Models.Correios
{
    public class CorreiosResponse
    {
        [JsonPropertyName("versao")]
        public string Versao { get; set; }

        [JsonPropertyName("quantidade")]
        public string Quantidade { get; set; }

        [JsonPropertyName("pesquisa")]
        public string Pesquisa { get; set; }

        [JsonPropertyName("resultado")]
        public string Resultado { get; set; }

        [JsonPropertyName("objeto")]
        public List<Objeto> Objeto { get; set; }
    }
}