using System.Text.Json.Serialization;

namespace Pnts.Tile
{
    public class FeatureTableMetadata
    {
        [JsonPropertyName("POINTS_LENGTH")]
        public int points_length { get; set; }
        public Position Position { get; set; }
        public Rgb Rgb { get; set; }

        [JsonPropertyName("RTC_CENTER")]
        public float[] Rtc_Center { get; set; }
    }
}
