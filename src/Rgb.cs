using System.Text.Json.Serialization;

namespace Pnts.Tile;

public class Rgb
{
    [JsonPropertyName("byteOffset")]

    public int ByteOffset { get; set; }
}
