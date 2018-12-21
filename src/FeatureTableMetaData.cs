namespace Pnts.Tile
{
    public class FeatureTableMetadata
    {
        public int points_length { get; set; }
        public Position Position { get; set; }
        public Rgb Rgb { get; set; }
        public float[] Rtc_Center { get; set; }
    }

    public class Position
    {
        public int byteOffset { get; set; }
    }

    public class Rgb
    {
        public int byteOffset { get; set; }
    }
}
