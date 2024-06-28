using System.Text.Json.Serialization;

namespace Pnts.Tile;

public class ByteOffset
{
    [JsonPropertyName("byteOffset")]

    public int byteOffset { get; set; }

    public bool Equals(ByteOffset other)
    {
        if (other == null)
        {
            return false;
        }
        return byteOffset == other.byteOffset;
    }

}
