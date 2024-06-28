namespace Pnts.Tile;

public class Point
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public bool Equals(Point other)
    {
        if (other == null)
        {
            return false;
        }
        return X == other.X && Y == other.Y && Z == other.Z;
    }
}
