using System.Text.Json.Serialization;

namespace RastreioBot.Models.Correios
{
    public class Objeto
    {
        [JsonPropertyName("numero")]
        public string Numero { get; set; }

        [JsonPropertyName("sigla")]
        public string Sigla { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("categoria")]
        public string Categoria { get; set; }

        [JsonPropertyName("evento")]
        public List<Evento> Evento { get; set; }
    }
}