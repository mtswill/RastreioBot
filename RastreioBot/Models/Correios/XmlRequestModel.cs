namespace RastreioBot.Models.Correios
{
    public static class XmlRequestModel
    {
        public const string XmlModel = "<rastroObjeto><usuario>{}</usuario><senha>{}</senha><tipo>L</tipo><resultado>T</resultado><objetos>@tracking_code_list</objetos><lingua>101</lingua><token>{}</token></rastroObjeto>";
    }
}