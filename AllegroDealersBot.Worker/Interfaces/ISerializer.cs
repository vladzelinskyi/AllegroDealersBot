using AllegroDealersBot.Models;

namespace AllegroDealersBot.Interfaces;

internal interface ISerializer
{
    public string SerializeToJson(YmlCatalog catalog);
    public YmlCatalog DeserializeJson(string json);
    public YmlCatalog DeserializeXml(string xml);
}