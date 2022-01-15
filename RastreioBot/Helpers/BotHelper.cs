using RastreioBot.Models.Correios;

namespace RastreioBot.Helpers
{
    public static class BotHelper
    {
        public static (string Result, bool IsSuccess) ToResponse(this CorreiosResponse correiosResponse)
        {
            var response = string.Empty;

            foreach (var obj in correiosResponse.Objeto)
            {
                if (obj.Evento == null)
                    return ("Objeto não encontrado na base de dados dos correios! Digite o código novamente ou digite 'sair' para não rastrear.", false);

                foreach (var trackingEvent in obj.Evento)
                {
                    var destiny = trackingEvent?.Destino?.FirstOrDefault();

                    if (destiny == null)
                        continue;

                    response += $"{obj.Numero}\n\n";

                    var state = trackingEvent?.Unidade.Uf != null ? $"- {trackingEvent.Unidade.Uf}" : string.Empty;
                    var destinyCity = destiny?.Cidade != null ? $"- {destiny?.Cidade}" : string.Empty;
                    var destinyState = destiny?.Uf != null ? $"/ {destiny?.Uf}" : string.Empty;
                    var destinyLocal = destiny?.Local != null ? $"{destiny?.Local}" : string.Empty;

                    var to = $"{destinyLocal} {destinyCity}{destinyState}".Trim();

                    response += $"{ConvertStringDateToDateTime(trackingEvent?.Data!, trackingEvent?.Hora!).ToShortDateString()}\n";
                    response += $"De: {trackingEvent?.Unidade.Cidade ?? trackingEvent?.Unidade.Local} {state}\n";
                    response += to != null ? $"Para: {to}\n" : string.Empty;
                    response += $"{trackingEvent?.Descricao}\n\n";

                    response += "----------------------------------------------------------------------------------\n\n";
                }
            }

            return (response, true);
        }

        private static DateTime ConvertStringDateToDateTime(string date, string hour)
        {
            if (date == null || hour == null)
                return DateTime.Now;

            var dateArray = date.Split("/");
            var hourArray = hour.Split(":");

            var day = int.Parse(dateArray[0]);
            var month = int.Parse(dateArray[1]);
            var year = int.Parse(dateArray[2]);
            var hours = int.Parse(hourArray[0]);
            var minutes = int.Parse(hourArray[1]);

            return new DateTime(year, month, day, hours, minutes, 0);
        }
    }
}
