using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace AllegroDealersBot.Models;

public class Offer
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlElement("price")]
    public int Price { get; set; }

    [XmlElement("oldprice")]
    public int OldPrice { get; set; }

    [XmlElement("name")]
    public string Name { get; set; } = string.Empty;

    [XmlElement("model")]
    public string Model { get; set; } = string.Empty;

    [XmlElement("vendor")]
    public string Vendor { get; set; } = string.Empty;

    [XmlElement("vendorCode")]
    public string VendorCode { get; set; } = string.Empty;

    [XmlElement("stock_quantity")]
    public int StockQuantity { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public string Status { get; set; } = string.Empty;

    [XmlIgnore]
    [JsonIgnore]
    public bool IsPriceChanged { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public bool IsStockChanged { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public bool RequiresImmediateAttention { get; set; } = false;
}