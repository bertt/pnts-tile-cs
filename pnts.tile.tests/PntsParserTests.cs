using NUnit.Framework;
using System.IO;
using System.Reflection;
using Pnts.Tile;

namespace Tests
{
    public class PntsParserTests
    {
        Stream pntsfile;

        [SetUp]
        public void Setup()
        {
            const string testfile = "Pnts.Tile.Tests.testfixtures.1-0-1-1.pnts";
            pntsfile = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            Assert.IsTrue(pntsfile != null);
        }

        [Test]
        public void ParsePntsTest()
        {
            // arrange
            var expectedMagicHeader = "pnts";
            var expectedVersionHeader = 1;

            // act
            var pnts = PntsParser.ParsePnts(pntsfile);

            // assert
            Assert.IsTrue(expectedMagicHeader == pnts.Magic);
            Assert.IsTrue(expectedVersionHeader == pnts.Version);
            Assert.IsTrue(pnts.Points != null);
            Assert.IsTrue(pnts.Points.Count > 0);
            Assert.IsTrue(pnts.Points[0].X == (float)144.78);
            Assert.IsTrue(pnts.Points[0].Y == (float)-64.85);
            Assert.IsTrue(pnts.Points[0].Z == (float)-174.68);
            Assert.IsTrue(pnts.Colors != null);
            Assert.IsTrue(pnts.Colors.Count > 0);
            Assert.IsTrue(pnts.Colors[0].R == 75);
            Assert.IsTrue(pnts.Colors[0].G == 91);
            Assert.IsTrue(pnts.Colors[0].B == 88);
            Assert.IsTrue(pnts.FeatureTableMetadata.points_length == 146);
        }
    }
}