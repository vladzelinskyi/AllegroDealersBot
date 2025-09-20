using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace AllegroDealersBot.Models;

[XmlRoot("yml_catalog", IsNullable = false)]
public class YmlCatalog
{
    [XmlElement("shop")]
    public Shop Shop { get; set; } = new Shop();
}