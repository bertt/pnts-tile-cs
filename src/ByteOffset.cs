using System.Text.Json.Serialization;

namespace Pnts.Tile
{
    public class ByteOffset
    {
        [JsonPropertyName("byteOffset")]

        public int offset { get; set; }
    }
}
