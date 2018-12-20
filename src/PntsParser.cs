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

                // first 4 bytes must be 'pnts' otherwise its not a pnts file
                var magic = Encoding.UTF8.GetString(reader.ReadBytes(4));
                var version = reader.ReadUInt32();

                var pnts = new Pnts() { Magic = magic, Version = (int)version };
                return pnts;
            }
        }
    }
}
