using System.Text.Json.Serialization;

namespace RastreioBot.Models.Correios
{
    public class Evento
    {
        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("hora")]
        public string Hora { get; set; }

        [JsonPropertyName("criacao")]
        public string Criacao { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("unidade")]
        public Unidade Unidade { get; set; }

        [JsonPropertyName("destino")]
        public List<Destino> Destino { get; set; }

        [JsonPropertyName("cepDestino")]
        public string CepDestino { get; set; }

        [JsonPropertyName("prazoGuarda")]
        public string PrazoGuarda { get; set; }

        [JsonPropertyName("diasUteis")]
        public string DiasUteis { get; set; }

        [JsonPropertyName("dataPostagem")]
        public string DataPostagem { get; set; }

        [JsonPropertyName("postagem")]
        public Postagem Postagem { get; set; }
    }
}