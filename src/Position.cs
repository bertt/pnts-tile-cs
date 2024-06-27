using System.Text.Json.Serialization;

namespace Pnts.Tile;

public class Position
{
    [JsonPropertyName("byteOffset")]

    public int ByteOffset { get; set; }
}
