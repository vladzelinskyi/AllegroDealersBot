using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml.Serialization;
using AllegroDealersBot.Interfaces;
using AllegroDealersBot.Models;

namespace AllegroDealersBot.Services;

internal class CatalogSerializer : ISerializer
{
    private XmlSerializer _serializer;

    public CatalogSerializer()
    {
        _serializer = new XmlSerializer(typeof(YmlCatalog));
    }

    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
    };

    public string SerializeToJson(YmlCatalog catalog)
    {
        return JsonSerializer.Serialize(catalog, options);
    }

    public YmlCatalog DeserializeJson(string json)
    {
        // todo: Handle JsonSerializer Exceptions
        return JsonSerializer.Deserialize<YmlCatalog>(json, options) ?? new YmlCatalog();
    }

    public YmlCatalog DeserializeXml(string xml)
    {
        YmlCatalog catalog;

        using (Stream reader = ConvertStringToStream(xml))
        {
            catalog = (YmlCatalog)_serializer.Deserialize(reader);
        }

        return catalog;
    }

    private MemoryStream ConvertStringToStream(string str)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(str);
        writer.Flush();
        stream.Position = 0;

        return stream;
    }
}