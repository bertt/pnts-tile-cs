using System.IO;
using System.Text;

namespace Pnts.Tile
{
    public static class PntsParser
    {
        public static Pnts ParsePnts(Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                // spec: https://github.com/AnalyticalGraphicsInc/3d-tiles/tree/master/specification/TileFormats/PointCloud
                // feature table spec: https://github.com/AnalyticalGraphicsInc/3d-tiles/blob/master/specification/TileFormats/FeatureTable/README.md

                // first 4 bytes must be 'pnts' otherwise its not a pnts file
                var magic = Encoding.UTF8.GetString(reader.ReadBytes(4));
                var version = reader.ReadUInt32();
                var tileByteLength = reader.ReadUInt32();  // 2608 (this is 28 (fro header) + 120 (for ft json) + 2460 (for ft bin))
                var featureTableJsonByteLength = reader.ReadUInt32(); // 120
                var featureTableBinByteLength = reader.ReadUInt32(); // 2460
                var batchTableJsonByteLength = reader.ReadUInt32(); // 0
                var batchTableBinByteLength = reader.ReadUInt32(); // 0

                var featureTableJsonBytes = reader.ReadBytes((int)featureTableJsonByteLength);
                var featureTableJson = Encoding.UTF8.GetString(featureTableJsonBytes); // "{\"POINTS_LENGTH\":164,\"POSITION\":{\"byteOffset\":0},\"RGB\":{\"byteOffset\":1968},\"RTC_CENTER\":[3830004.5,323597.5,5072948.5]}\n"

                var featureTableBinBytes = reader.ReadBytes((int)featureTableBinByteLength);

                // q: what to do with the featureTableBinBytes ?
                // `POINTS_LENGTH`| `uint32` | The number of points to render.
                // dus 164 punten * 3 (x,y,z) * 4 (1 float 4 bytes) = 1968 bytes (en dan beginnen de RGB kleuren)
                // todo: read first 1968 bytes of featureTableBinBytes and convert to 164 points.

                var pnts = new Pnts() { Magic = magic, Version = (int)version };
                return pnts;
            }
        }
    }
}
