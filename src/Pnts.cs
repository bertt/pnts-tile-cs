using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;

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
        var header_length = 28;

        var featureTableJson = JsonSerializer.Serialize(FeatureTableMetadata);
        var batchTableJson = BatchTable.Keys.Count> 0? JsonSerializer.Serialize(BatchTable): String.Empty;

        // convert points and colors to binary
        var featureTableBinary = ToFeatureTableBytes(Points, Colors);
        featureTableJson = BufferPadding.AddPadding(featureTableJson);
        batchTableJson = BufferPadding.AddPadding(batchTableJson);
        var batchTableBinary = new byte[0];

        var byteLength = header_length + featureTableJson.Length + Encoding.UTF8.GetByteCount(batchTableJson) + batchTableBinary.Length + featureTableBinary.Length;

        var featureTableJsonByteLength = featureTableJson.Length;
        var batchTableJsonByteLength = Encoding.UTF8.GetByteCount(batchTableJson);
        var featureTableBinaryByteLength = featureTableBinary.Length;
        var batchTableBinaryByteLength = batchTableBinary.Length;

        var memoryStream = new MemoryStream();
        var binaryWriter = new BinaryWriter(memoryStream);
        binaryWriter.Write(Encoding.UTF8.GetBytes(Magic));
        binaryWriter.Write(BitConverter.GetBytes(Version));
        binaryWriter.Write(BitConverter.GetBytes(byteLength));
        binaryWriter.Write(BitConverter.GetBytes(featureTableJsonByteLength));
        binaryWriter.Write(BitConverter.GetBytes(featureTableBinaryByteLength));
        binaryWriter.Write(BitConverter.GetBytes(batchTableJsonByteLength));
        binaryWriter.Write(BitConverter.GetBytes(batchTableBinaryByteLength));
        binaryWriter.Write(Encoding.UTF8.GetBytes(featureTableJson));
        binaryWriter.Write(featureTableBinary);
        binaryWriter.Flush();
        binaryWriter.Close();
        return memoryStream.ToArray();
    }

    public bool Equals(Pnts other)
    {
        if (other == null)
        {
            return false;
        }

        return
            Magic == other.Magic &&
            Version == other.Version &&
            Colors.Count == other.Colors.Count &&
            Points.Count == other.Points.Count &&
            FeatureTableMetadata.Equals(other.FeatureTableMetadata) &&
            BatchTable.Keys.Count == other.BatchTable.Keys.Count;
    }

    private static byte[] ToFeatureTableBytes(IEnumerable<Point> points, IEnumerable<Color> colors)
    {
        var featureTableMemoryStream = new MemoryStream();
        var binaryWriter = new BinaryWriter(featureTableMemoryStream);
        foreach (var point in points)
        {
            binaryWriter.Write(point.X);
            binaryWriter.Write(point.Y);
            binaryWriter.Write(point.Z);
        }
        foreach (var color in colors)
        {
            binaryWriter.Write(color.R);
            binaryWriter.Write(color.G);
            binaryWriter.Write(color.B);
        }
        binaryWriter.Flush();
        binaryWriter.Close();
        var featureTableBinary = featureTableMemoryStream.ToArray();
        return featureTableBinary;
    }
}
