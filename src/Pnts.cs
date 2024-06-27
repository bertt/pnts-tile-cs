using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pnts.Tile;

public class Pnts
{
    public string Magic { get; set; }
    public int Version { get; set; }
    public List<Point> Points { get; set; }
    public List<Color> Colors { get; set; }
    public FeatureTable FeatureTableMetadata { get; set; }

    public Dictionary<string, byte[]> BatchTable { get; set; }

    public byte[] ToBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(Encoding.UTF8.GetBytes(Magic));
        bytes.AddRange(BitConverter.GetBytes(Version));
        bytes.AddRange(BitConverter.GetBytes(Points.Count));

        foreach (var point in Points)
        {
            bytes.AddRange(BitConverter.GetBytes(point.X));
            bytes.AddRange(BitConverter.GetBytes(point.Y));
            bytes.AddRange(BitConverter.GetBytes(point.Z));
        }

        foreach (var color in Colors)
        {
            bytes.Add(color.R);
            bytes.Add(color.G);
            bytes.Add(color.B);
        }

        // todo feature Table

        return bytes.ToArray();

    }
}
