using System.Text.Json.Serialization;

namespace Pnts.Tile;

public class FeatureTable
{
    // todo: POSITION_QUANTIZED, RGBA, RGB565, NORMAL, NORMAL_OCT16P, BATCH_ID, QUANTIZED_VOLUME_OFFSET, QUANTIZED_VOLUME_SCALE, CONSTANT_RGBA,BATCH_LENGTH

    [JsonPropertyName("POINTS_LENGTH")]
    public int pointsLength { get; set; }
    [JsonPropertyName("POSITION")]
    public ByteOffset PositionByteOffset { get; set; }
    [JsonPropertyName("RGB")]
    public ByteOffset RgbByteOffset { get; set; }
    [JsonPropertyName("RTC_CENTER")]
    public float[] Rtc_Center { get; set; }

    // Add equals method
    public bool Equals(FeatureTable other)
    {
        if (other == null)
        {
            return false;
        }
        return
            pointsLength == other.pointsLength &&
            RgbByteOffset.Equals(other.RgbByteOffset) &&
            PositionByteOffset.Equals(other.PositionByteOffset) &&
            Rtc_Center[0].Equals(other.Rtc_Center[0]) &&
            Rtc_Center[1].Equals(other.Rtc_Center[1]) &&
            Rtc_Center[2].Equals(other.Rtc_Center[2]);
    }
}
