using NUnit.Framework;
using System.IO;
using Pnts.Tile;

namespace Tests;

public class PntsReaderTests
{
    [Test]
    public void ReadGoCesiumTilerPnts()
    {
        const string testfile = "testfixtures/content.pnts";
        var pntsFile = File.OpenRead(testfile);
        var pnts = PntsReader.Read(pntsFile);
    }

    [Test]
    public void TestPointCloudWithPerPointProperties()
    {
        const string testfile = "testfixtures/pointCloudWithPerPointProperties.pnts";
        var pntsFile = File.OpenRead(testfile);
        var pnts = PntsReader.Read(pntsFile);
    }

    [Test]
    public void TestPointsFile()
    {
        // source file: https://github.com/CesiumGS/3d-tiles-samples/blob/master/tilesets/TilesetWithRequestVolume/points.pnts
        var pntsFile = File.OpenRead(@"testfixtures/points.pnts");
        var pnts = PntsReader.Read(pntsFile);
        var expectedMagicHeader = "pnts";
        var  expectedVersionHeader = 1;

        Assert.That(expectedMagicHeader == pnts.Magic);
        Assert.That(expectedVersionHeader == pnts.Version);
        Assert.That(pnts.Points.Count == 125000);
        Assert.That(pnts.Colors.Count == 125000);
        Assert.That(pnts.Points[0].X == (float)-1.1413337);
        Assert.That(pnts.Points[0].Y == (float)0.359452039);
        Assert.That(pnts.Points[0].Z == (float)-0.361457467);
        Assert.That(pnts.Colors[0].R == 182);
        Assert.That(pnts.Colors[0].G == 215);
        Assert.That(pnts.Colors[0].B == 153);
    }

    [Test]
    public void ParsePntsTest()
    {
        // arrange
        const string testfile = "testfixtures/1-0-1-1.pnts";
        var pntsfile = File.OpenRead(testfile);
        var expectedMagicHeader = "pnts";
        var expectedVersionHeader = 1;


        // act
        var pnts = PntsReader.Read(pntsfile);

        // assert
        Assert.That(expectedMagicHeader == pnts.Magic);
        Assert.That(expectedVersionHeader == pnts.Version);
        Assert.That(pnts.Points != null);
        Assert.That(pnts.Points.Count > 0);
        Assert.That(pnts.Points[0].X == (float)144.78);
        Assert.That(pnts.Points[0].Y == (float)-64.85);
        Assert.That(pnts.Points[0].Z == (float)-174.68);
        Assert.That(pnts.Colors != null);
        Assert.That(pnts.Colors.Count > 0);
        Assert.That(pnts.Colors[0].R == 75);
        Assert.That(pnts.Colors[0].G == 91);
        Assert.That(pnts.Colors[0].B == 88);
        Assert.That(pnts.FeatureTableMetadata.pointsLength == 164);
        var rtc = pnts.FeatureTableMetadata.Rtc_Center;
        Assert.That(rtc[0] == 3830004.5);
        Assert.That(rtc[1] == 323597.5);
        Assert.That(rtc[2] == 5072948.5);
    }
}