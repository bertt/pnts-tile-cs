using NUnit.Framework;
using System.IO;
using Pnts.Tile;

namespace Tests
{
    public class Id
    {
        public int byteOffset { get; set; }
        public string componentType { get; set; }
        public string type { get; set; }
    }

    public class PntsParserTests
    {
        Stream pntsfile;
        string expectedMagicHeader = "pnts";
        int expectedVersionHeader = 1;

        [SetUp]
        public void Setup()
        {
            const string testfile = "testfixtures/1-0-1-1.pnts";
            pntsfile = File.OpenRead(testfile);
            Assert.That(pntsfile != null);
        }

        [TearDown]
        public void TearDown()
        {
            pntsfile.Close();
        }

        [Test]
        public void ReadGoCesiumTilerPnts()
        {
            const string testfile = "testfixtures/content.pnts";
            var pntsFile = File.OpenRead(testfile);
            var pnts = PntsSerializer.Deserialize(pntsFile);


        }

        [Test]
        public void TestPointCloudWithPerPointProperties()
        {
            const string testfile = "testfixtures/pointCloudWithPerPointProperties.pnts";
            var pntsFile = File.OpenRead(testfile);
            var pnts = PntsSerializer.Deserialize(pntsFile);
        }

        [Test]
        public void TestPointsFile()
        {
            // source file: https://github.com/CesiumGS/3d-tiles-samples/blob/master/tilesets/TilesetWithRequestVolume/points.pnts
            var pntsFile = File.OpenRead(@"testfixtures/points.pnts");
            var pnts = PntsSerializer.Deserialize(pntsFile);
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

            // act
            var pnts = PntsSerializer.Deserialize(pntsfile);

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
            Assert.That(pnts.FeatureTableMetadata.points_length == 164);
            var rtc = pnts.FeatureTableMetadata.Rtc_Center;
            Assert.That(rtc[0] == 3830004.5);
            Assert.That(rtc[1] == 323597.5);
            Assert.That(rtc[2] == 5072948.5);
        }

        [Test]
        public void FeatureTableDeserialize()
        {
            var featureTableJson = "{\"POINTS_LENGTH\":125000,\"POSITION\":{\"byteOffset\":0},\"RGB\":{\"byteOffset\":1500000}}       ";
            var featureTableMetadata = System.Text.Json.JsonSerializer.Deserialize<FeatureTable>(featureTableJson);
            Assert.That(featureTableMetadata.points_length == 125000);
        }
    }
}