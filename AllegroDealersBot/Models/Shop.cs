using System.Xml.Serialization;

namespace AllegroDealersBot.Models;

public class Shop
{
    [XmlElement("name")]
    public string Name { get; set; } = string.Empty;

    [XmlElement("company")]
    public string Company { get; set; } = string.Empty;

    [XmlElement("url")]
    public string Url { get; set; } = string.Empty;

    [XmlArray("offers")]
    [XmlArrayItem("offer")]
    public List<Offer> Offers { get; set; } = new List<Offer>();
}