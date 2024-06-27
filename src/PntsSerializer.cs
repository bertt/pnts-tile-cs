using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Pnts.Tile;

public static class PntsSerializer
{
    public static Pnts Deserialize(Stream stream)
    {
        using (var reader = new BinaryReader(stream))
        {
            // spec: https://github.com/AnalyticalGraphicsInc/3d-tiles/tree/master/specification/TileFormats/PointCloud
            // feature table spec: https://github.com/AnalyticalGraphicsInc/3d-tiles/blob/master/specification/TileFormats/FeatureTable/README.md

            // first 4 bytes must be 'pnts' otherwise its not a pnts file
            var magic = Encoding.UTF8.GetString(reader.ReadBytes(4));
            var version = reader.ReadUInt32();
            var tileByteLength = reader.ReadUInt32();  
            var featureTableJsonByteLength = reader.ReadUInt32(); 
            var featureTableBinByteLength = reader.ReadUInt32();
            var batchTableJsonByteLength = reader.ReadUInt32();
            var batchTableBinByteLength = reader.ReadUInt32();

            var featureTableJsonBytes = reader.ReadBytes((int)featureTableJsonByteLength);
            var featureTableJson = Encoding.UTF8.GetString(featureTableJsonBytes);
            var featureTableMetadata = JsonSerializer.Deserialize<FeatureTable>(featureTableJson);

            var featureTableBinBytes = reader.ReadBytes((int)featureTableBinByteLength);

            var featureTableStream = new MemoryStream(featureTableBinBytes);
            var binaryReader = new BinaryReader(featureTableStream);

            var points = new List<Point>();
            for (var i = 0; i < featureTableMetadata.points_length; i++)
            {
                var x = binaryReader.ReadSingle();
                var y = binaryReader.ReadSingle();
                var z = binaryReader.ReadSingle();

                var p = new Point { X = x, Y = y, Z = z };
                points.Add(p);
            }

            var colors = new List<Color>();
            for (var i = 0; i < featureTableMetadata.points_length; i++)
            {
                var r = (int)binaryReader.ReadByte();
                var g = (int)binaryReader.ReadByte();
                var b = (int)binaryReader.ReadByte();

                var c = Color.FromArgb(r, g, b);
                colors.Add(c);
            }

            var batchTable = new Dictionary<string, byte[]>();
            if (batchTableJsonByteLength > 0)
            {
                var batchTableJsonBytes = reader.ReadBytes((int)batchTableJsonByteLength);
                var batchTableJson = Encoding.UTF8.GetString(batchTableJsonBytes);
                var batchTableBinaryBodyReferences = JsonSerializer.Deserialize<Dictionary<string, BinaryBodyReference>>(batchTableJson);

                var batchTableBinBytes = reader.ReadBytes((int)batchTableBinByteLength);

                foreach (var reference in batchTableBinaryBodyReferences)
                {
                    var offset = reference.Value.byteOffset;
                    var componentType = reference.Value.componentType;
                    var type = reference.Value.type;

                    var key = reference.Key;
                    var values = new List<byte>();

                    for (var i = 0; i < featureTableMetadata.points_length; i++)
                    { 

                        if (type == "SCALAR")
                        {
                            if (componentType == "UNSIGNED_BYTE")
                            {
                                var value = batchTableBinBytes[offset + i];
                                values.Add(value);
                            }
                            else if (componentType == "UNSIGNED_SHORT")
                            {
                            }
                            else if (componentType == "FLOAT")
                            {
                            }

                        }
                        else if (type == "VEC3")
                        {
                        }
                    }

                   batchTable.Add(key, values.ToArray());   
                }
            }

            var pnts = new Pnts() { Magic = magic, Version = (int)version, Points = points, Colors = colors, FeatureTableMetadata = featureTableMetadata, BatchTable = batchTable };
            return pnts;
        }
    }
}
