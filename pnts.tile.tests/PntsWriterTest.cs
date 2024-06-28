using NUnit.Framework;
using System.IO;

namespace Pnts.Tile.Tests;
public class PntsWriterTest
{
    [Test]
    public void WritePntsFile()
    {
        // arrange
        const string testfile = "testfixtures/1-0-1-1.pnts";
        var pntsfile = File.OpenRead(testfile);
        var pnts = PntsReader.Read(pntsfile);

        // act
        var pntsBytes = pnts.ToBytes();
        File.WriteAllBytes(@"d:\aaa\1-0-1-1.pnts", pntsBytes);
        // read back to memory stream
        var ms = new MemoryStream(pntsBytes);
        var pnts2 = PntsReader.Read(ms);

        // assert
        Assert.That(pnts.Magic == pnts2.Magic);
        Assert.That(pnts.Version == pnts2.Version);
        Assert.That(pnts.FeatureTableMetadata.Equals(pnts2.FeatureTableMetadata));
        Assert.That(pnts.Equals(pnts2));


        // pnts.FeatureTableMetadata.AssertEqual(pnts2.FeatureTableMetadata);

        // Assert.That(pnts.Points == pnts2.Points);





    }

}
